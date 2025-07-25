using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Schedule;
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private const string Message = "Id parametru jest niepoprawne.";
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }
        public async Task<ScheduleForEntryVm> GetAllSchedulesAsync()
        {
            var schedules = await _scheduleRepository.GetAllSchedulesAsync();
            var scheduleVms = schedules.AsQueryable()
                .ProjectTo<ScheduleEntryVm>(_mapper.ConfigurationProvider)
                .ToList();
            return new ScheduleForEntryVm
            {
                Schedules = scheduleVms,
                Count = scheduleVms.Count
            };
        }
       
        public async Task<IEnumerable<ScheduleEntryVm>> GetSchedulesByIdAsync(int employeeId, DateTime start, DateTime end)
        {
            var entries = await _scheduleRepository.GetByTeacherAsync(employeeId);
            return entries
                .Where(x => x.StartTime < end && x.EndTime > start)
                .AsQueryable()
                .ProjectTo<ScheduleEntryVm>(_mapper.ConfigurationProvider)
                .ToList();
        }
        public async Task<IEnumerable<ScheduleEntryVm>> GetSchedulesByRangeAsync(DateTime start, DateTime end)
        {
            var entries = await _scheduleRepository.GetByDateRangeAsync(start, end);
            return entries
                .AsQueryable()
                .ProjectTo<ScheduleEntryVm>(_mapper.ConfigurationProvider)
                .ToList();
        }
       
        public async Task<int> AddScheduleAsync(NewScheduleEntryVm entryVm)
        {
            if (!IsScheduleEntryValid(entryVm))
                throw new InvalidOperationException("Konflikt godzinowy");

            if (entryVm.StartTime > entryVm.EndTime)
                throw new InvalidOperationException("Czas rozpoczęcia musi być wcześniejszy niż czas zakończenia");

            if (entryVm.EntryType == ScheduleEntryTypeEnum.GODZINY_STALE)
            {
                if (entryVm.RangeStart > entryVm.RangeEnd)
                    throw new InvalidOperationException("Data początkowa musi być wcześniejsza niż data końcowa");

            
                var startDate = entryVm.RangeStart.Date;
                var endDate = entryVm.RangeEnd.Date;
                var entries = new List<ScheduleEntry>();

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == entryVm.DayOfWeek)
                    {
                        var entity = _mapper.Map<ScheduleEntry>(entryVm);
                        entity.StartTime = date.Date.Add(entryVm.StartTime.TimeOfDay);
                        entity.EndTime = date.Date.Add(entryVm.EndTime.TimeOfDay);
                        entries.Add(entity);
                    }
                }
                if (entries.Count == 0)
                    return 0;

                await _scheduleRepository.AddScheduleEntriesAsync(entries);

                return entries.Last().Id;
            }
            else
            {
                var entity = _mapper.Map<ScheduleEntry>(entryVm);
                return await _scheduleRepository.AddScheduleEntryAsync(entity);
            }
        }
        public async Task<int> AddScheduleAsync(EditScheduleEntryVm entryVm)
        {
            if (!IsScheduleEntryValid(entryVm))
                throw new InvalidOperationException("Konflikt godzinowy");
            if (entryVm.StartTime > entryVm.EndTime)
                throw new InvalidOperationException("Czas rozpoczęcia musi być wcześniejszy niż czas zakończenia");
            var entity = _mapper.Map<ScheduleEntry>(entryVm);
            return await _scheduleRepository.AddScheduleEntryAsync(entity);
        }
        public async Task UpdateScheduleAsync(EditScheduleEntryVm entryVm)
        {
            if (!IsScheduleEntryValid(entryVm))
                throw new InvalidOperationException("Konflikt godzinowy");

            var entity = _mapper.Map<ScheduleEntry>(entryVm);
            await _scheduleRepository.UpdateScheduleEntryAsync(entity);
        }

        public Task DeleteScheduleAsync(int scheduleEntryid)
        {
            if (scheduleEntryid <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(scheduleEntryid),
                    scheduleEntryid,
                    Message
                );
            }
            return _scheduleRepository.DeleteScheduleEntryAsync(scheduleEntryid);
        }

        public bool IsScheduleEntryValid(NewScheduleEntryVm newEntryVm)
        {
            var existing = _scheduleRepository.GetByTeacher(newEntryVm.EmployeeId)
                .Where(e => e.StartTime.Date == newEntryVm.StartTime.Date);

            foreach (var e in existing)
            {
                bool overlap = newEntryVm.StartTime < e.EndTime && newEntryVm.EndTime > e.StartTime;
                if (overlap)
                    return false;
            }

            return true;
        }
        public bool IsScheduleEntryValid(EditScheduleEntryVm EditEntryVm)
        {
            var existing = _scheduleRepository.GetByTeacher(EditEntryVm.EmployeeId)
                .Where(e => e.StartTime.Date == EditEntryVm.StartTime.Date && e.Id != EditEntryVm.Id);

            foreach (var e in existing)
            {
                bool overlap = EditEntryVm.StartTime < e.EndTime && EditEntryVm.EndTime > e.StartTime;
                if (overlap)
                    return false;
            }

            return true;
        }
    }
}
