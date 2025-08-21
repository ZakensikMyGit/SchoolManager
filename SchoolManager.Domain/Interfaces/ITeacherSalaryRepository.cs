using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Interfaces
{
    public interface ITeacherSalaryRepository
    {
        Task<TeacherSalary?> GetByPeriodAsync(int schoolYearStart, SemesterEnum semester);
        Task<TeacherSalary?> GetWithAllowancesAsync(int id);
        Task<int> AddAsync(TeacherSalary entity);
        Task UpdateAsync(TeacherSalary entity);
        Task ReplaceAllowancesAsync(int teacherSalaryId, IEnumerable<MotivationalAllowance> items);
    }
}