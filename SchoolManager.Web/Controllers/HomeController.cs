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

        [Route("Employees")]
        public IActionResult ViewListOfEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            
            return View(employees);
        }

        [Route("Teachers")]
        public IActionResult ViewListOfTeachers()
        {
            var teachers = _employeeService.GetAllTeachers();
            
            return View(teachers);
        }

        [Route("Detail/{id:int}")]
        public IActionResult ViewEmployeeDetails(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            return View(employee);
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
