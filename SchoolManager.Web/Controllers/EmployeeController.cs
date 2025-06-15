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
        private readonly IGroupRepository _groupRepository;

        public EmployeeController(
            IEmployeeService employeeService,
            IPositionRepository positionRepository,
            IGroupRepository groupRepository)
        {
            _employeeService = employeeService;
            _positionRepository = positionRepository;
            _groupRepository = groupRepository;
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
                }),
                Groups = _groupRepository.GetAllGroups().Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.GroupName
                })
            });
        }

        [HttpPost]
        public IActionResult AddEmployee(NewEmployeeVm model)
        {
            var positions = _positionRepository.GetAllPositions();
            var groups = _groupRepository.GetAllGroups();
            model.Positions = positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            model.Groups = groups.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
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
            employeeModel.Groups = _groupRepository.GetAllGroups().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
            });
            return View(employeeModel);
        }
        [HttpPost]
        public IActionResult EditEmployee(NewEmployeeVm model)
        {
            var positions = _positionRepository.GetAllPositions();
            var groups = _groupRepository.GetAllGroups();
            model.Positions = positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            model.Groups = groups.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
            });
            var id = _employeeService.AddEmployee(model);
            return RedirectToAction("Index");
        }
        public IActionResult DetailsEmployee(int id)
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
