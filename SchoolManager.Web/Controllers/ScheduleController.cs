using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Schedule;
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using static System.Net.Mime.MediaTypeNames;

namespace SchoolManager.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmployeeRepository _employeeRepository;

        public ScheduleController(
            IScheduleService scheduleService,
            IEmployeeRepository employeeRepository)
        {
            _scheduleService = scheduleService;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> Index(int employeeId, DateTime? start, DateTime? end)
        {
            if (employeeId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Nieprawidłowy identyfikator pracownika.");
                return View();
            }
            if (start == null || end == null)
            {
                start = DateTime.Today;
                end = DateTime.Today.AddDays(7);
            }
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
            if (start > end)
            {
                return BadRequest("Data początkowa nie może być późniejsza niż data końcowa.");
            }

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
                "kotki" => "group-kotki",
                _ => string.Empty
            };
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var groups = Enum.GetValues(typeof(GroupEnum)).Cast<GroupEnum>().Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.ToString()
            });

            var model = new NewScheduleEntryVm
            {
                Employees = (await _employeeRepository.GetAllActiveEmployeesAsync()).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.LastName} {e.FirstName}"
                }),

                Groups = groups,
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1),
                DayOfWeek = DayOfWeek.Monday,
                RangeStart = DateTime.Today,
                RangeEnd = DateTime.Today,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewScheduleEntryVm model)
        {
            if (model == null)
            {
                return BadRequest("Model nie może być null");
            }
            model.Employees = (await _employeeRepository.GetAllActiveEmployeesAsync()).Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.LastName} {e.FirstName}"
            });
            model.Groups = Enum.GetValues(typeof(GroupEnum)).Cast<GroupEnum>().Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.ToString()
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