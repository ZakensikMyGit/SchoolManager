using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly Context _context;
        public ScheduleRepository(Context context)
        {
            _context = context;
        }

        public int AddScheduleEntry(ScheduleEntry entry)
        {
            _context.ScheduleEntries.Add(entry);
            _context.SaveChanges();
            return entry.Id;
        }

        public void DeleteScheduleEntry(int id)
        {
            var entry = _context.ScheduleEntries.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                _context.ScheduleEntries.Remove(entry);
                _context.SaveChanges();
            }
        }

        public ScheduleEntry GetById(int id)
        {
            return _context.ScheduleEntries
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .Include(e => e.Group)
                .FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<ScheduleEntry> GetByGroup(int groupId)
        {
            return _context.ScheduleEntries
                .Where(e => e.GroupId == groupId);
        }

        public IQueryable<ScheduleEntry> GetByTeacher(int emloyeeId)
        {
            return _context.ScheduleEntries
                .Where(e => e.EmployeeId == emloyeeId);
        }

        public void UpdateScheduleEntry(ScheduleEntry entry)
        {
            _context.ScheduleEntries.Update(entry);
            _context.SaveChanges();
        }
    }
}
