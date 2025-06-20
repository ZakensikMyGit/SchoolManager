using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Schedule;
using SchoolManager.Domain.Interfaces;

namespace SchoolManager.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGroupRepository _groupRepository;

        public ScheduleController(
            IScheduleService scheduleService,
            IEmployeeRepository employeeRepository,
            IGroupRepository groupRepository)
        {
            _scheduleService = scheduleService;
            _employeeRepository = employeeRepository;
            _groupRepository = groupRepository;
        }

        public IActionResult Index(int employeeId, DateTime? start, DateTime? end)
        {
            if (start == null) start = DateTime.Today.AddDays(-7);
            if (end == null) end = DateTime.Today.AddDays(7);
            var model = _scheduleService.GetSchedules(employeeId, start.Value, end.Value);
            ViewBag.EmployeeId = employeeId;
            return View(model);
        }
        [HttpGet]
        public IActionResult GetEvents(int employeeId, DateTime start, DateTime end)
        {
            var events = _scheduleService.GetSchedules(employeeId, start, end)
                .Select(e => new
                {
                    id = e.Id,
                    title = string.Format(
                        "{0:HH:mm} - {1:HH:mm} {2} - {3}",
                        e.StartTime,
                        e.EndTime,
                        e.GroupName,
                        GetInitialAndLastName(e.EmployeeName)
                    ),
                    start = e.StartTime,
                    end = e.EndTime,
                    description = e.Description
                });
            return Json(events);
        }
        private static string GetInitialAndLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return string.Empty;
            }
            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                return string.Empty;
            }

            var firstInitial = parts[0].Substring(0, 1).ToUpperInvariant();
            var lastName = parts.Length > 1 ? parts[^1] : string.Empty;
            return $"{firstInitial}.{lastName}";
        }

        [HttpGet]
        public IActionResult Add(int employeeId)
        {
            var model = new NewScheduleEntryVm
            {
                EmployeeId = employeeId,
                Employees = _employeeRepository.GetAllActiveEmployees().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }),
                
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(NewScheduleEntryVm model)
        {
            model.Employees = _employeeRepository.GetAllActiveEmployees().Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FullName
            });
            var employee = _employeeRepository.GetEmployee(model.EmployeeId);
            if (employee != null)
            {
                model.PositionId = employee.PositionId;
                var group = _groupRepository.GetAllGroups().FirstOrDefault(g => g.TeacherId == employee.Id);
                if (group != null)
                {
                    model.GroupId = group.Id;
                }
            }

            try
            {
                _scheduleService.AddSchedule(model);
                return RedirectToAction("Index", new { employeeId = model.EmployeeId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}