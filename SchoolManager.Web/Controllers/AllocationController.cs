using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Allocation;
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Model;

namespace SchoolManager.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class AllocationController : Controller
    {
        private readonly ITeacherSalaryService _teacherSalaryService;
        private readonly IEmployeeService _employeeService;

        public AllocationController(
            ITeacherSalaryService teacherSalaryService,
            IEmployeeService employeeService)
        {
            _teacherSalaryService = teacherSalaryService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int schoolYearStart, SemesterEnum semester)
        {
            var sheet = await _teacherSalaryService.GenerateOrGetAsync(schoolYearStart, semester);

            var all = await _employeeService.GetAllActiveEmployeeAsync();
            var teachers = all.OfType<Teacher>()
                              .Where(t => t.IsActive && !t.IsDirector)
                              .OrderBy(t => t.LastName)
                              .ThenBy(t => t.FirstName)
                              .ToList();

            var items = new List<AllocationVm.ItemVm>();
            foreach (var t in teachers)
            {
                var allowance = sheet.AllowancesHistory?.FirstOrDefault(a => a.TeacherId == t.Id);
                var percent = allowance?.Percentage ?? 0;
                var baseSalary = t.BaseSalary ?? 0m;
                var amount = Round2((percent / 100m) * baseSalary);

                items.Add(new AllocationVm.ItemVm
                {
                    TeacherId = t.Id,
                    TeacherName = t.FullName,
                    BaseSalary = baseSalary,
                    Percent = percent,
                    Amount = amount
                });
            }

            var assigned = Round2(items.Sum(i => i.Amount));
            var vm = new AllocationVm
            {
                TeacherSalaryId = sheet.Id,
                SchoolYearStart = sheet.SchoolYearStart,
                Semester = sheet.Semester,
                Budget = Round2(sheet.TotalAmount),
                Assigned = assigned,
                IsApproved = sheet.IsApproved,
                Items = items
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Dyrektor")]
        public async Task<IActionResult> SaveDraft([FromBody] SaveDraftVm request)
        {
            if (request is null || request.Items is null)
                return BadRequest(new { ok = false, message = "Brak danych do zapisu." });

            var result = await _teacherSalaryService.SaveDraftAsync(request.TeacherSalaryId,
                request.Items.Select(i => (i.TeacherId, i.Percent)));

            return Json(new
            {
                ok = result.ok,
                sum = result.sum,
                remaining = result.remaining,
                message = result.message
            });
        }

        [HttpPost]
        [Authorize(Roles = "Dyrektor")]
        public async Task<IActionResult> Approve([FromBody] ApproveVm request)
        {
            if (request is null || request.TeacherSalaryId <= 0)
                return BadRequest(new { ok = false, message = "Brak identyfikatora arkusza." });

            var ok = await _teacherSalaryService.ApproveAsync(request.TeacherSalaryId, User.Identity?.Name ?? "system");
            var sheet = await _teacherSalaryService.GetAsync(request.TeacherSalaryId);

            var sum = Round2(sheet.AllowancesHistory?.Sum(a => a.Amount) ?? 0m);
            var remaining = Round2(sheet.TotalAmount - sum);

            return Json(new
            {
                ok,
                sum,
                remaining,
                message = ok ? "Podział zatwierdzony." : "Nie można zatwierdzić — przekroczony budżet lub arkusz już zatwierdzony."
            });
        }

        [HttpPost]
        [Authorize(Roles = "Dyrektor")]
        public async Task<IActionResult> ResetToSeven([FromBody] ApproveVm request)
        {
            if (request is null || request.TeacherSalaryId <= 0)
                return BadRequest(new { ok = false, message = "Brak identyfikatora arkusza." });

            var sheet = await _teacherSalaryService.GetAsync(request.TeacherSalaryId);
            if (sheet.IsApproved)
                return BadRequest(new { ok = false, message = "Arkusz jest zatwierdzony." });

            var all = await _employeeService.GetAllActiveEmployeeAsync();
            var teachers = all.OfType<Teacher>().Where(t => !t.IsDirector).ToList();
            var items = teachers.Select(t => (t.Id, Percent: 7));

            var result = await _teacherSalaryService.SaveDraftAsync(sheet.Id, items);

            return Json(new { ok = result.ok, sum = result.sum, remaining = result.remaining, message = "Ustawiono 7% dla wszystkich." });
        }

        [HttpGet]
        [Authorize(Roles = "Dyrektor, Ksiegowosc")]
        public async Task<IActionResult> ExportPdf(int arkuszId)
        {
            var bytes = await Task.FromResult(Array.Empty<byte>());
            return File(bytes, "application/pdf", $"przydzial_{arkuszId}.pdf");
        }

        [HttpGet]
        [Authorize(Roles = "Dyrektor, Ksiegowosc")]
        public async Task<IActionResult> ExportExcel(int arkuszId)
        {
            var bytes = await Task.FromResult(Array.Empty<byte>());
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"przydzial_{arkuszId}.xlsx");
        }

        private static decimal Round2(decimal x) =>
            Math.Round(x, 2, MidpointRounding.AwayFromZero);
    }
}