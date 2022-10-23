using Microsoft.AspNetCore.Mvc;

namespace myCourse.Controllers
{
   public class CoursesController : Controller
   {
      public IActionResult Index()
      {
         return Content("I am Index");
      }

      public IActionResult Detail(string id)
      {
         return Content($"detail id : {id}");
      }


   }
}