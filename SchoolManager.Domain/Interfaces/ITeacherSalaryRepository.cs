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
            Task<int> AddAsync(TeacherSalary teacherSalary);
        }
}
