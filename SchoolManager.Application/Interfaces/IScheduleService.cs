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
        ScheduleForEntryVm GetAllSchedules();
        IEnumerable<ScheduleEntryVm> GetSchedulesById(int employeeId, DateTime start, DateTime end);
        int AddSchedule(NewScheduleEntryVm editEntryVm);
        void UpdateSchedule(EditScheduleEntryVm editEntryVm);
        void DeleteSchedule(int scheduleEntryid);
        bool IsScheduleEntryValid(NewScheduleEntryVm newEntryVm);
        bool IsScheduleEntryValid(EditScheduleEntryVm editEntryVm);

        Task<ScheduleForEntryVm> GetAllSchedulesAsync();
        Task<IEnumerable<ScheduleEntryVm>> GetSchedulesByIdAsync(int employeeId, DateTime start, DateTime end);
        Task<int> AddScheduleAsync(NewScheduleEntryVm editEntryVm);
        Task UpdateScheduleAsync(EditScheduleEntryVm editEntryVm);
        Task DeleteScheduleAsync(int scheduleEntryid);
    }
}
