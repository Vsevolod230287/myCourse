using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using myCourse.Models.ViewModels;
using myCourse.Models.Services.Infrastructure;
using Microsoft.Extensions.Options;
using myCourse.Models.Options;
using Microsoft.Extensions.Logging;
using myCourse.Models.Exceptions;

namespace myCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        private readonly ILogger<AdoNetCourseService> logger;

        public AdoNetCourseService(ILogger<AdoNetCourseService> logger, IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.db = db;
            this.coursesOptions = coursesOptions;
            this.logger = logger;
        }


        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {

            logger.LogInformation("Course {id} requested.", id);

            FormattableString query = $@"select Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount,
            CurrentPrice_Currency from Courses where Id={id}; Select Id, Title, Description, Duration from Lessons where CourseId={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Course
            var courseTable = dataSet.Tables[0];

            if (courseTable.Rows.Count != 1)
            {
                logger.LogWarning("Course {id} not found", id);
                throw new CourseNotFoundException(id);
            }

            var courseRow = courseTable.Rows[0];

            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Course Lessons
            var lessonsDataTable = dataSet.Tables[0];

            foreach (DataRow lessonRow in lessonsDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }
            return courseDetailViewModel;
        }



        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            FormattableString query = $"Select Id, Title,Description, ImagePath,Author, Email, Rating, FullPrice_Amount, FullPrice_Currency,  CurrentPrice_Amount, CurrentPrice_Currency from Courses";

            DataSet dataSet = await db.QueryAsync(query);





            var dataTable = dataSet.Tables[0];

            var courseList = new List<CourseViewModel>();

            foreach (DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }

            return courseList;
        }
    }
}

