using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Services;

namespace SchoolManager.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var model = _employeeService.GetAllEmployeesForList(); // Przykładowe wywołanie serwisu, aby pobrać pracowników o danym stanowisku
            return View(model);
        }

      
    }
}
