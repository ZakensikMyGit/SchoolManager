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
        Task<TeacherSalary> GenerateSalaryAsync();
    }
}
