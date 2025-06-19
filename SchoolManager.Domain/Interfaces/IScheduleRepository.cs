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
        IQueryable<ScheduleEntry> GetByGroup(int groupId);
        ScheduleEntry GetById(int id);
        int AddScheduleEntry(ScheduleEntry entry);
        void UpdateScheduleEntry(ScheduleEntry entry);
        void DeleteScheduleEntry(int id);
    }
}
