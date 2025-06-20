using Microsoft.AspNetCore.Mvc;

namespace SchoolManager.Web.Views.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
