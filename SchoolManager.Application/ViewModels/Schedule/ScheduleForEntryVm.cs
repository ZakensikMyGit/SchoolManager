using SchoolManager.Application.ViewModels.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Schedule
{
    public class ScheduleForEntryVm
    {
        public List<ScheduleEntryVm> Schedules { get; set; }
        public int Count { get; set; }
    }
}
