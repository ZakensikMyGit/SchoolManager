using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Interfaces
{
    public interface ITeacherSalaryService
    {
        Task<TeacherSalary> GenerateOrGetAsync(int schoolYearStart, SemesterEnum semester);
        Task<(bool ok, decimal sum, decimal remaining, string message)> SaveDraftAsync(int teacherSalaryId, IEnumerable<(int teacherId, int percent)> items);
        Task<bool> ApproveAsync(int teacherSalaryId, string user);
        Task<TeacherSalary> GetAsync(int id);
    }
}