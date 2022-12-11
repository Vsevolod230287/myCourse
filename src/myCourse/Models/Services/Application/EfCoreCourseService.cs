using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myCourse.Models.ViewModels;
using myCourse.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace myCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MyCourceDbContext dbContext;

        public EfCoreCourseService(MyCourceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            CourseDetailViewModel viewModel = await dbContext.Courses
               .AsNoTracking()
               .Where(course => course.Id == id)
               .Select(course => new CourseDetailViewModel
               {
                   Id = course.Id,
                   Title = course.Title,
                   ImagePath = course.ImagePath,
                   Author = course.Author,
                   Description = course.Description,
                   Rating = course.Rating,
                   CurrentPrice = course.CurrentPrice,
                   FullPrice = course.FullPrice,
                   Lessons = course.Lessons.Select(lesson => new LessonViewModel
                   {
                       Id = lesson.Id,
                       Title = lesson.Title,
                       Description = lesson.Description
                       // Duration = lesson.Duration,
                   }).ToList()
               })
               .SingleAsync();
            //FirstOrDefultAsync // non soleva eccezioni, restiuisce null se l'eleco e vuoto
            //.SingleAsync();      //eccezione se l'elenco e vuoto e se contiene più elementi
            //.SingleOrDefault();  // ecezione solo se contiene piu elementi, se e vuoto restituisce il default del tipo => che per i tipi complessi e null, se restituirebbe un int allora il default del interi e 0 
            //.FirstAsync();       // ecezione solo se l'elenco e vuoto
            return viewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> queryLink = dbContext.Courses
            .AsNoTracking()
            .Select(course =>
            new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                ImagePath = course.ImagePath,
                Author = course.Author,
                Rating = course.Rating,
                CurrentPrice = course.CurrentPrice,
                FullPrice = course.FullPrice
            });

            List<CourseViewModel> courses = await queryLink.ToListAsync();
            return courses;
        }
    }
}