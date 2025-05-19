using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Services;
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
            IEmployeeService employeeService = new EmlpoyeeService();
            var employees = employeeService.GetAllEmployees();
            return View(employees);
        }

        public IActionResult ViewListOfEmployees()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John", LastName = "Doe", IsActive = true },
                new Employee { Id = 2, Name = "Jane", LastName = "Smith", IsActive = false },
                new Employee { Id = 3, Name = "Sam", LastName = "Brown", IsActive = true }
            };

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
