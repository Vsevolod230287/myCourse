using System;
using myCourse.Models.ViewModels;
using System.Collections.Generic;
using myCourse.Models.ValueTypes;
using myCourse.Models.Enums;
using System.Threading.Tasks;

namespace myCourse.Models.Services.Application
{
    public class CourseService : ICourseService
    {
        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            var courseList = new List<CourseViewModel>();
            var rand = new Random();

            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                    Author = "Nome Cognome",
                    Rating = rand.NextDouble() * 5.0,
                    ImagePath = "/logo.png"
                };
                courseList.Add(course);
            }
            return courseList;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            var rand = new Random();

            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);

            var course = new CourseDetailViewModel()
            {
                Id = id,
                Title = $"Corso {id}",
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price),
                Rating = rand.Next(10, 50) / 10.0,
                ImagePath = "/logo.png",
                Lessons = new List<LessonViewModel>(),

            };

            for (int i = 1; i <= 5; i++)
            {
                var lesson = new LessonViewModel
                {
                    Title = $"Lezione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90)),
                };
                course.Lessons.Add(lesson);
            }

            return course;
        }

    }
}
