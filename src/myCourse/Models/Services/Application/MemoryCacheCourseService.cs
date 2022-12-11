using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using myCourse.Models.ViewModels;

namespace myCourse.Models.Services.Application
{
    public class MemoryCacheCourseService : ICashedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheCourseService(ICourseService courseService, IMemoryCache memoryCache)
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
        }

        // usare memoryCache.Remove($"Course{id}"); quando aggiorni un corso 
        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync()
        {
            return memoryCache.GetOrCreateAsync("Courses", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetCoursesAsync();
            });
        }
    }
}