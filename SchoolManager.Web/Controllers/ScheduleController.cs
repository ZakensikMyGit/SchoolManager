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
                    title = e.GroupName,
                    start = e.StartTime,
                    end = e.EndTime,
                    description = e.Description
                });
            return Json(events);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var model = new NewScheduleEntryVm
            {
                EmployeeId = id,
                Employees = _employeeRepository.GetAllActiveEmployees().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }),
                Groups = _groupRepository.GetAllGroups().Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.GroupName
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
            model.Groups = _groupRepository.GetAllGroups().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.GroupName
            });

            _scheduleService.AddSchedule(model);
            return RedirectToAction("Index", new { employeeId = model.EmployeeId });
        }
    }
}