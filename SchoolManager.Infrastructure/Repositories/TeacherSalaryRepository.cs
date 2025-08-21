using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Enums;
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
        private readonly Context _ctx;
        public TeacherSalaryRepository(Context ctx)
        {
            _ctx = ctx;
        }

        public async Task<TeacherSalary?> GetByPeriodAsync(int schoolYearStart, SemesterEnum semester)
        {
            return await _ctx.TeacherSalaries
                .Include(x => x.AllowancesHistory)
                .FirstOrDefaultAsync(x => x.SchoolYearStart == schoolYearStart && x.Semester == semester);
        }

        public async Task<TeacherSalary?> GetWithAllowancesAsync(int id)
        {
            return await _ctx.TeacherSalaries
                .Include(x => x.AllowancesHistory)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddAsync(TeacherSalary entity)
        {
            _ctx.TeacherSalaries.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(TeacherSalary entity)
        {
            _ctx.TeacherSalaries.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task ReplaceAllowancesAsync(int teacherSalaryId, IEnumerable<MotivationalAllowance> items)
        {
            var existing = _ctx.MotivationalAllowances.Where(a => a.TeacherSalaryId == teacherSalaryId);
            _ctx.MotivationalAllowances.RemoveRange(existing);
            await _ctx.MotivationalAllowances.AddRangeAsync(items);
            await _ctx.SaveChangesAsync();
        }
    }
}
