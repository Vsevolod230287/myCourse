using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using myCourse.Models.ViewModels;
using Newtonsoft.Json;

namespace myCourse.Models.Services.Application
{
    public class DistributedCacheCourseService : ICashedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheCourseService(ICourseService courseService, IDistributedCache distributedCache)
        {
            this.courseService = courseService;
            this.distributedCache = distributedCache;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {

            throw new NotImplementedException();
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            string key = $"Courses";
            string serializedObject = await distributedCache.GetStringAsync(key);

            if (serializedObject != null)
            {
                return Deserialize<List<CourseViewModel>>(serializedObject);
            }

            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            serializedObject = Serialize(courses);

            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
            await distributedCache.SetStringAsync(key, serializedObject, cacheOptions);
            return courses;


        }





        private string Serialize(object obj)
        {
            //Convertiamo un oggetto in una stringa
            return JsonConvert.SerializeObject(obj);
        }

        private T Deserialize<T>(string serializedObject)
        {
            //Riconvertiamo una stringa in un oggetto
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }




    }
}