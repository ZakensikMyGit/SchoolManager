using SchoolManager.Application.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Interfaces
{
    public interface IScheduleService
    {
        IEnumerable<ScheduleEntryVm> GetSchedules(int employeeId, DateTime strart, DateTime end);
        int AddSchedule(NewScheduleEntryVm entryVm);
        void UpdateSchedule(EditScheduleEntryVm entryVm);
        void DeleteSchedule(int scheduleEntryid);
        bool IsScheduleEntryValid(NewScheduleEntryVm entryVm);
        bool IsScheduleEntryValid(EditScheduleEntryVm entryVm);
    }
}
