using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.Services;
using SchoolManager.Application.ViewModels.Employee;

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


        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View(new NewEmployeeVm());
        }
        [HttpPost]
        public IActionResult AddEmployee(NewEmployeeVm model)
        {
            var id = _employeeService.AddEmployee(model);
            return RedirectToAction("index");
        }
        public IActionResult ViewEmployee(int employeeId)
        {
            var employeeModel = _employeeService.GetEmployeeDetails(employeeId);
            return View(employeeModel);
        }
    }
}
