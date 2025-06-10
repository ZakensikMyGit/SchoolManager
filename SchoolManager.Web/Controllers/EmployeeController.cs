using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Employee;
using SchoolManager.Domain.Interfaces;

namespace SchoolManager.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPositionRepository _positionRepository;

        public EmployeeController(
            IEmployeeService employeeService,
            IPositionRepository positionRepository)
        {
            _employeeService = employeeService;
            _positionRepository = positionRepository;
        }
        public IActionResult Index()
        {
            var model = _employeeService.GetAllEmployeesForList(); // Przykładowe wywołanie serwisu, aby pobrać pracowników o danym stanowisku
            return View(model);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View(new NewEmployeeVm
            {
                Positions = _positionRepository.GetAllPositions().Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
            });
        }

        [HttpPost]
        public IActionResult AddEmployee(NewEmployeeVm model)
        {
            var positions = _positionRepository.GetAllPositions();
            model.Positions = positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            var id = _employeeService.AddEmployee(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var employeeModel = _employeeService.GetEmployeeForEdit(id);
            employeeModel.Positions = _positionRepository.GetAllPositions().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            return View(employeeModel);
        }
        [HttpPost]
        public IActionResult EditEmployee(NewEmployeeVm model)
        {
            var positions = _positionRepository.GetAllPositions();
            model.Positions = positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            var id = _employeeService.AddEmployee(model);
            return RedirectToAction("Index");
        }
        public IActionResult ViewEmployee(int id)
        {
            var employeeModel = _employeeService.GetEmployeeDetails(id);
            return View(employeeModel);
        }

        public IActionResult Delete(int Id)
        {
            _employeeService.DeleteEmployee(Id);
            return RedirectToAction("Index");
        }
    }
}
