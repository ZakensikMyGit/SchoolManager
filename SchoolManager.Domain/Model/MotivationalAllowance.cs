using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class MotivationalAllowance
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public DateTime SemesterStart { get; set; }
        public DateTime SemesterEnd { get; set; }
        public int Percentage { get; set; }
        public decimal Amount { get; set; }
        public int? TeacherSalaryId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual TeacherSalary TeacherSalary { get; set; }
    }
}
