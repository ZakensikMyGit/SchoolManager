using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Domain.Interfaces
{
    public interface IScheduleRepository
    {
        IQueryable<ScheduleEntry> GetAllSchedules();
        IQueryable<ScheduleEntry> GetByTeacher(int emloyeeId);
        IQueryable<ScheduleEntry> GetByGroup(int groupId);
        ScheduleEntry GetById(int id);
        int AddScheduleEntry(ScheduleEntry entry);
        void UpdateScheduleEntry(ScheduleEntry entry);
        void DeleteScheduleEntry(int id);

        Task<List<ScheduleEntry>> GetAllSchedulesAsync();
        Task<List<ScheduleEntry>> GetByTeacherAsync(int employeeId);
        Task<List<ScheduleEntry>> GetByGroupAsync(int groupId);
        Task<ScheduleEntry?> GetByIdAsync(int id);
        Task<int> AddScheduleEntryAsync(ScheduleEntry entry);
        Task UpdateScheduleEntryAsync(ScheduleEntry entry);
        Task DeleteScheduleEntryAsync(int id);
        Task<List<ScheduleEntry>> GetByDateRangeAsync(DateTime start, DateTime end);
    }
}
