using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Allocation
{
    public class AllocationVm
    {
        public int TeacherSalaryId { get; set; }
        public int SchoolYearStart { get; set; }
        public SemesterEnum Semester { get; set; }
        public string SemesterName => Semester.ToString().Replace('_', '-');
        public decimal Budget { get; set; }
        public decimal Assigned { get; set; }
        public bool IsApproved { get; set; }
        public byte[]? RowVersion { get; set; }
        public List<ItemVm> Items { get; set; } = new();

        public class ItemVm
        {
            public int TeacherId { get; set; }
            public string TeacherName { get; set; } = string.Empty;
            public decimal BaseSalary { get; set; }

            [Range(0, 100, ErrorMessage = "Procent 0–100")]
            public int Percent { get; set; }

            public decimal Amount { get; set; }
        }
    }
}