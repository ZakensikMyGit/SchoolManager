using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Model
{
    public class TeacherSalary
    {
        public int Id { get; set; }
        public int SchoolYearStart { get; set; }
        public SemesterEnum Semester { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedBy { get; set; }
        public ICollection<MotivationalAllowance> AllowancesHistory { get; set; } = new List<MotivationalAllowance>();
    }
}