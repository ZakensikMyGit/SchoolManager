using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure.Repositories
{
    public class TeacherSalaryRepository : ITeacherSalaryRepository

    {
        private readonly Context _context;
        public TeacherSalaryRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(TeacherSalary teacherSalary)
        {
            await _context.TeacherSalaries.AddAsync(teacherSalary);
            await _context.SaveChangesAsync();
            return teacherSalary.Id;
        }
    }
}
