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
            //if (start == null) start = DateTime.Today.AddDays(-7);
            //if (end == null) end = DateTime.Today.AddDays(7);
            //var model = _scheduleService.GetSchedulesById(employeeId, start.Value, end.Value);
            //ViewBag.EmployeeId = employeeId;
            //return View(model);
            ViewBag.EmployeeId = employeeId;
            var model = _scheduleService.GetAllSchedules();
            return View(model);
        }
        [HttpGet]
        public IActionResult GetEvents(int? employeeId, DateTime start, DateTime end)
        {
            //var events = _scheduleService.GetSchedulesById(employeeId, start, end)
            //    .Select(e => new
            //    {
            //        id = e.Id,
            //        title = string.Format(
            //            "{0:HH:mm} - {1:HH:mm} {2}",
            //            e.StartTime,
            //            e.EndTime,
            //            GetInitialAndLastName(e.EmployeeName)
            //        ),
            //        start = e.StartTime,
            //        end = e.EndTime,
            //        description = e.Description,
            //        className = GetGroupClass(e.GroupName)
            //    });
            IEnumerable<ScheduleEntryVm> entries;
            if (employeeId.HasValue && employeeId.Value != 0)
            {
                entries = _scheduleService.GetSchedulesById(employeeId.Value, start, end);
            }
            else
            {
                entries = _scheduleService
                    .GetAllSchedules()
                    .Schedules
                    .Where(e => e.StartTime < end && e.EndTime > start);
            }

            var events = entries.Select(e => new
            {
                id = e.Id,
                title = string.Format(
                    "{0:HH:mm} - {1:HH:mm} {2}",
                    e.StartTime,
                    e.EndTime,
                    GetInitialAndLastName(e.EmployeeName)
                ),
                start = e.StartTime,
                end = e.EndTime,
                description = e.Description,
                className = GetGroupClass(e.GroupName)
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

        private static string GetGroupClass(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                return string.Empty;
            }

            if (string.Equals(groupName, "smerfy", StringComparison.OrdinalIgnoreCase))
            {
                return "group-smerfy";
            }

            if (string.Equals(groupName, "motyle", StringComparison.OrdinalIgnoreCase))
            {
                return "group-motyle";
            }

            return string.Empty;
        }


        [HttpGet]
        public IActionResult Add(int employeeId)
        {
            var groups = _groupRepository.GetAllGroups().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
            });

            var defaultGroup = _groupRepository.GetAllGroups().FirstOrDefault(g => g.TeacherId == employeeId);
            var model = new NewScheduleEntryVm
            {
                EmployeeId = employeeId,
                Employees = _employeeRepository.GetAllActiveEmployees().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }),

                Groups = groups,
                GroupId = defaultGroup?.Id ?? 0,
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
            model.Groups = _groupRepository.GetAllGroups().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
            });
            var employee = _employeeRepository.GetEmployee(model.EmployeeId);
            if (employee != null)
            {
                model.PositionId = employee.PositionId;
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