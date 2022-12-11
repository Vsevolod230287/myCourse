using Microsoft.AspNetCore.Mvc;
using myCourse.Models.Services.Application;
using myCourse.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICashedCourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            ViewBag.Title = "Catalogo dei Corsi";

            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewBag.Title = viewModel.Title;
            return View(viewModel);
        }


    }
}