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
        public async Task<IActionResult> Index()
        {
            var model = await _employeeService.GetAllEmployeesForListAsync();
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
        public async Task<IActionResult> AddEmployee(NewEmployeeVm model)
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
            var id = await _employeeService.AddEmployeeAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employeeModel = await _employeeService.GetEmployeeForEditAsync(id);
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
        public async Task<IActionResult> EditEmployee(NewEmployeeVm model)
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
            var id = await _employeeService.AddEmployeeAsync(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DetailsEmployee(int id)
        {
            var employeeModel = await _employeeService.GetEmployeeDetailsAsync(id);
            return View(employeeModel);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _employeeService.DeleteEmployeeAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
