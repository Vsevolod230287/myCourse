using Microsoft.AspNetCore.Mvc;

namespace myCourse.Controllers
{
   public class HomeController : Controller
   {
      public IActionResult Index()
      {
         return Content("I am Index of Home");
      }
   }
}