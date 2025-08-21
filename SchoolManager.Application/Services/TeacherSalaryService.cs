using SchoolManager.Application.Interfaces;
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Services
{
    public class TeacherSalaryService : ITeacherSalaryService
    {
        private readonly ITeacherSalaryRepository _repo;
        private readonly IEmployeeRepository _employees;

        public TeacherSalaryService(ITeacherSalaryRepository repo, IEmployeeRepository employees)
        {
            _repo = repo;
            _employees = employees;
        }

        public async Task<TeacherSalary> GenerateOrGetAsync(int schoolYearStart, SemesterEnum semester)
        {
            var existing = await _repo.GetByPeriodAsync(schoolYearStart, semester);
            if (existing != null) return existing;

            var employees = await _employees.GetAllActiveEmployeesAsync();
            var totalBase = employees
                .OfType<Teacher>()
                .Where(t => t.IsActive && !t.IsDirector)
                .Sum(t => t.BaseSalary ?? 0m);
            var budget = Round2(totalBase * 0.07m);

            var sheet = new TeacherSalary
            {
                SchoolYearStart = schoolYearStart,
                Semester = semester,
                TotalAmount = budget,
                IsApproved = false,
                AllowancesHistory = new List<MotivationalAllowance>()
            };

            sheet.Id = await _repo.AddAsync(sheet);
            return sheet;
        }

        public async Task<(bool ok, decimal sum, decimal remaining, string message)> SaveDraftAsync(int teacherSalaryId, IEnumerable<(int teacherId, int percent)> items)
        {
            var sheet = await _repo.GetWithAllowancesAsync(teacherSalaryId)
                       ?? throw new System.InvalidOperationException("Arkusz nie istnieje.");
            if (sheet.IsApproved)
                return (false, 0, 0, "Arkusz jest już zatwierdzony.");

            var employees = await _employees.GetAllActiveEmployeesAsync();
            var dict = employees.OfType<Teacher>().ToDictionary(t => t.Id, t => t.BaseSalary ?? 0m);

            var computed = new List<MotivationalAllowance>();
            foreach (var (teacherId, percent) in items)
            {
                var baseSalary = dict.TryGetValue(teacherId, out var bs) ? bs : 0m;
                var amount = Round2((percent / 100m) * baseSalary);
                computed.Add(new MotivationalAllowance
                {
                    TeacherSalaryId = teacherSalaryId,
                    TeacherId = teacherId,
                    Percentage = percent,
                    Amount = amount
                });
            }

            var sum = Round2(computed.Sum(a => a.Amount));
            var remaining = Round2(sheet.TotalAmount - sum);

            await _repo.ReplaceAllowancesAsync(teacherSalaryId, computed);

            return (true, sum, remaining, "Szkic zapisany.");
        }

        public async Task<bool> ApproveAsync(int teacherSalaryId, string user)
        {
            var sheet = await _repo.GetWithAllowancesAsync(teacherSalaryId)
                       ?? throw new System.InvalidOperationException("Arkusz nie istnieje.");
            if (sheet.IsApproved) return false;

            var employees = await _employees.GetAllActiveEmployeesAsync();
            var dict = employees.OfType<Teacher>().ToDictionary(t => t.Id, t => t.BaseSalary ?? 0m);

            decimal sum = 0m;
            foreach (var a in sheet.AllowancesHistory)
            {
                var baseSalary = dict.TryGetValue(a.TeacherId, out var bs) ? bs : 0m;
                a.Amount = Round2((a.Percentage / 100m) * baseSalary);
                sum += a.Amount;
            }
            sum = Round2(sum);

            if (sum > sheet.TotalAmount) return false;

            sheet.IsApproved = true;
            sheet.ApprovedAt = System.DateTime.UtcNow;
            sheet.ApprovedBy = user;

            await _repo.UpdateAsync(sheet);
            return true;
        }

        public async Task<TeacherSalary> GetAsync(int id)
            => await _repo.GetWithAllowancesAsync(id)
               ?? throw new System.InvalidOperationException("Arkusz nie istnieje.");

        private static decimal Round2(decimal x)
            => System.Math.Round(x, 2, System.MidpointRounding.AwayFromZero);
    }
}