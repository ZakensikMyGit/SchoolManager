using SchoolManager.Application.Interfaces;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITeacherSalaryRepository _teacherSalaryRepository;
        public TeacherSalaryService(IEmployeeRepository employeeRepository, ITeacherSalaryRepository teacherSalaryRepository)
        {
            _employeeRepository = employeeRepository;
            _teacherSalaryRepository = teacherSalaryRepository;
        }

        public async Task<TeacherSalary> GenerateSalaryAsync()
        {
            var employees = await _employeeRepository.GetAllActiveEmployeesAsync();
            var totalBase = employees
                .OfType<Teacher>()
                .Where(t => !t.IsDirector)
                .Sum(t => t.BaseSalary ?? 0m);
            var totalAmount = Math.Round(totalBase * 0.07m, 2, MidpointRounding.AwayFromZero);
            var salary = new TeacherSalary
            {
                TotalAmount = totalAmount,
                AllowancesHistory = new List<MotivationalAllowance>()
            };
            await _teacherSalaryRepository.AddAsync(salary);
            return salary;
        }

    }
}