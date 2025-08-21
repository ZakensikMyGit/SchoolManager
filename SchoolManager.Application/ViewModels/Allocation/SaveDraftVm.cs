using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Allocation
{
    public class SaveDraftVm
    {
        public int TeacherSalaryId { get; set; }
        public List<Item> Items { get; set; } = new();
        public record Item(int TeacherId, int Percent);
    }

    public class ApproveVm
    {
        public int TeacherSalaryId { get; set; }
    }
}