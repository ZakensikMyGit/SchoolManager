using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Employee;
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Interfaces;
using System.Linq;

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
        public async Task<IActionResult> Index()
        {
            var model = await _employeeService.GetAllEmployeesForListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            var positions = await _positionRepository.GetAllPositionsAsync();
            var groups = Enum.GetValues(typeof(GroupEnum)).Cast<GroupEnum>();

            return View(new NewEmployeeVm
            {
                Positions = positions.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }),
                Groups = groups.Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                })
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(NewEmployeeVm model)
        {
            if (model == null)
            {
                return BadRequest("NewEmployeeVM nie może być null");
            }
            var positions = await _positionRepository.GetAllPositionsAsync();
            var groups = Enum.GetValues(typeof(GroupEnum)).Cast<GroupEnum>();
            model.Positions = positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            model.Groups = groups.Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.ToString()
            });
            if (!ModelState.IsValid)
                return View(model);
            var id = await _employeeService.AddEmployeeAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id musi być większe niż 0");
            }
            var employeeModel = await _employeeService.GetEmployeeForEditAsync(id);
            var positions = await _positionRepository.GetAllPositionsAsync();
            var groups = Enum.GetValues(typeof(GroupEnum)).Cast<GroupEnum>();
            employeeModel.Positions = positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            employeeModel.Groups = groups.Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.ToString()
            });
            return View(employeeModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(NewEmployeeVm model)
        {
            if (model == null)
            {
                return BadRequest("NewEmployeeVM nie może być null");
            }
            var positions = await _positionRepository.GetAllPositionsAsync();
            var groups = Enum.GetValues(typeof(GroupEnum)).Cast<GroupEnum>();
            model.Positions = positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            });
            model.Groups = groups.Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.ToString()
            });
            if (!ModelState.IsValid)
                return View(model);

            var id = await _employeeService.AddEmployeeAsync(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DetailsEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id musi być większe niż 0");
            }
            var employeeModel = await _employeeService.GetEmployeeDetailsAsync(id);
            return View(employeeModel);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Id musi być większe niż 0");
            }
            await _employeeService.DeleteEmployeeAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
