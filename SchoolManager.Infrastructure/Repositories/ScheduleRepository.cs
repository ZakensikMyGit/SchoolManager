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

        public IQueryable<ScheduleEntry> GetAllSchedules()
        {
            return _context.ScheduleEntries
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .Include(e => e.Group);
        }

        public Task<List<ScheduleEntry>> GetAllSchedulesAsync()
        {
            return _context.ScheduleEntries
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .Include(e => e.Group)
                .ToListAsync();
        }
        public int AddScheduleEntry(ScheduleEntry entry)
        {
            _context.ScheduleEntries.Add(entry);
            _context.SaveChanges();
            return entry.Id;
        }

        public async Task<int> AddScheduleEntryAsync(ScheduleEntry entry)
        {
            await _context.ScheduleEntries.AddAsync(entry);
            await _context.SaveChangesAsync();
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

        public async Task DeleteScheduleEntryAsync(int id)
        {
            var entry = await _context.ScheduleEntries.FirstOrDefaultAsync(e => e.Id == id);
            if (entry != null)
            {
                _context.ScheduleEntries.Remove(entry);
                await _context.SaveChangesAsync();
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

        public Task<ScheduleEntry?> GetByIdAsync(int id)
        {
            return _context.ScheduleEntries
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .Include(e => e.Group)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<ScheduleEntry> GetByGroup(int groupId)
        {
            return _context.ScheduleEntries
                .Where(e => e.GroupId == groupId);
        }

        public Task<List<ScheduleEntry>> GetByGroupAsync(int groupId)
        {
            return _context.ScheduleEntries
                .Where(e => e.GroupId == groupId)
                .ToListAsync();
        }

        public IQueryable<ScheduleEntry> GetByTeacher(int emloyeeId)
        {
            return _context.ScheduleEntries
                .Where(e => e.EmployeeId == emloyeeId);
        }

        public Task<List<ScheduleEntry>> GetByTeacherAsync(int emloyeeId)
        {
            return _context.ScheduleEntries
                .Where(e => e.EmployeeId == emloyeeId)
                .ToListAsync();
        }

        public void UpdateScheduleEntry(ScheduleEntry entry)
        {
            _context.ScheduleEntries.Update(entry);
            _context.SaveChanges();
        }

        public async Task UpdateScheduleEntryAsync(ScheduleEntry entry)
        {
            _context.ScheduleEntries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public Task<List<ScheduleEntry>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            return _context.ScheduleEntries
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .Include(e => e.Group)
                .Where(e => e.StartTime < end && e.EndTime > start)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddScheduleEntriesAsync(IEnumerable<ScheduleEntry> entries)
        {
            await _context.ScheduleEntries.AddRangeAsync(entries);
            await _context.SaveChangesAsync();
        }
    }
}