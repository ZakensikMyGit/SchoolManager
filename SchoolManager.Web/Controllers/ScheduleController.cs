using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Schedule;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;

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

        public async Task<IActionResult> Index(int employeeId, DateTime? start, DateTime? end)
        {
            ViewBag.EmployeeId = employeeId;
            var model = await _scheduleService.GetAllSchedulesAsync();
            return View(model);
        }
        public async Task<IActionResult> Entries()
        {
            var model = await _scheduleService.GetAllSchedulesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(int? employeeId, DateTime start, DateTime end)
        {
            IEnumerable<ScheduleEntryVm> entries;
            if (employeeId.HasValue && employeeId.Value != 0)
            {
                entries = await _scheduleService.GetSchedulesByIdAsync(employeeId.Value, start, end);
            }
            else
            {
                entries = await _scheduleService.GetSchedulesByRangeAsync(start, end);
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
                return string.Empty;

            return groupName.Trim().ToLowerInvariant()
                switch
            {
                "smerfy" => "group-smerfy",
                "motyle" => "group-motyle",
                "krasnale" => "group-krasnale",
                "koty" => "group-koty",
                _ => string.Empty
            };
        }

        [HttpGet]
        public async Task<IActionResult> Add(int employeeId)
        {
            var group = await _groupRepository.GetAllGroupsAsync();
            var groups = group.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
            });

            var employee = await _employeeRepository.GetEmployeeAsync(employeeId) as Teacher;
            Group? defaultGroup = null;
            if (employee != null && employee.GroupId.HasValue)
            {
                defaultGroup = await _groupRepository.GetGroupAsync(employee.GroupId.Value);
            }
            var model = new NewScheduleEntryVm
            {
                EmployeeId = employeeId,
                Employees = (await _employeeRepository.GetAllActiveEmployeesAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }),

                Groups = groups,
                GroupId = defaultGroup?.Id ?? 0,
                StartTime = DateTime.Today,
                //EndTime = DateTime.Today.AddHours(1)
                EndTime = DateTime.Today.AddHours(1),
                DayOfWeek = DayOfWeek.Monday,
                RangeStart = DateTime.Today,
                RangeEnd = DateTime.Today.AddMonths(1),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewScheduleEntryVm model)
        {
            model.Employees = (await _employeeRepository.GetAllActiveEmployeesAsync()).Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FullName
            });
            model.Groups = (await _groupRepository.GetAllGroupsAsync()).Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
            });
            var employee = await _employeeRepository.GetEmployeeAsync(model.EmployeeId);
            if (employee != null)
            {
                model.PositionId = employee.PositionId ?? 0;
            }

            try
            {
                await _scheduleService.AddScheduleAsync(model);
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