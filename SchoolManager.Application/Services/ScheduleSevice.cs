using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Schedule;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Services
{
    public class ScheduleSevice : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        public ScheduleSevice(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }
        public IEnumerable<ScheduleEntryVm> GetSchedules(int employeeId, DateTime start, DateTime end)
        {
            return _scheduleRepository.GetByTeacher(employeeId)
                .Where(x => x.StartTime >= start && x.EndTime <= end)
                .ProjectTo<ScheduleEntryVm>(_mapper.ConfigurationProvider).ToList();
        }
        public int AddSchedule(NewScheduleEntryVm entryVm)
        {
            if (!IsScheduleEntryValid(entryVm))
                throw new InvalidOperationException("Konflikt godzinowy");

            if (entryVm.StartTime > entryVm.EndTime)
                throw new InvalidOperationException("Czas rozpoczęcia musi być wcześniejszy niż czas zakończenia");

            if (entryVm.PositionId != 5)
                throw new InvalidOperationException("Pracownik musi być nauczycielem");

            var entity = _mapper.Map<ScheduleEntry>(entryVm);
            return _scheduleRepository.AddScheduleEntry(entity);
        }
        public void UpdateSchedule(EditScheduleEntryVm entryVm)
        {
            if (!IsScheduleEntryValid(entryVm))
                throw new InvalidOperationException("Konflikt godzinowy");

            var entity = _mapper.Map<ScheduleEntry>(entryVm);
            _scheduleRepository.UpdateScheduleEntry(entity);
        }

        public void DeleteSchedule(int scheduleEntryid)
        {
            _scheduleRepository.DeleteScheduleEntry(scheduleEntryid);
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
