using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Web.Interfaces;
using SchoolManager.Web.Models;

namespace SchoolManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("all")]
        public IActionResult ViewListOfEmployees()
        {
            var employees = _employeeService.GetAllEmployees;
            
            return View(employees);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
