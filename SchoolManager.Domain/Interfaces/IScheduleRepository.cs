using SchoolManager.Domain.Enums;
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
        IQueryable<ScheduleEntry> GetByTeacher(int emloyeeId);

        Task<List<ScheduleEntry>> GetAllSchedulesAsync();
        Task<List<ScheduleEntry>> GetByTeacherAsync(int employeeId);
        Task<List<ScheduleEntry>> GetByGroupAsync(GroupEnum group);
        Task<ScheduleEntry?> GetByIdAsync(int id);
        Task<int> AddScheduleEntryAsync(ScheduleEntry entry);
        Task UpdateScheduleEntryAsync(ScheduleEntry entry);
        Task DeleteScheduleEntryAsync(int id);
        Task<List<ScheduleEntry>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task AddScheduleEntriesAsync(IEnumerable<ScheduleEntry> entries);
    }
}
