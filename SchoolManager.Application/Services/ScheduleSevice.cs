using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolManager.Application.Interfaces;
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
        public IEnumerable<ScheduleEntryVm> GetSchedules(int employeeId, DateTime strart, DateTime end)
        {
            return _scheduleRepository.GetByTeacher(employeeId)
                .Where(x => x.StartTime >= strart && x.EndTime <= end)
                .ProjectTo<ScheduleEntryVm>(_mapper.ConfigurationProvider).ToList();
        }
        public int AddSchedule(NewScheduleEntryVm entryVm)
        {
            if (!IsScheduleEntryValid(entryVm))
                throw new InvalidOperationException("Konflikt godzinowy");
             
            if (entryVm.StartTime > entryVm.EndTime)
                throw new InvalidOperationException("Czas rozpoczęcia musi być wcześniejszy niż czas zakończenia");

            //sprawdzenie czy positionId pracoewnika jest równe 5
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


        public bool IsScheduleEntryValid(NewScheduleEntryVm entryVm)
        {
            var existing = _scheduleRepository.GetByTeacher(entryVm.TeacherId)
                .Where(e => e.StartTime.Date == entryVm.StartTime.Date);

            foreach (var e in existing)
            {
                bool overlap = entryVm.StartTime < e.EndTime && entryVm.EndTime > e.StartTime;
                if (overlap)
                    return false;
            }

            return true;
        }
    }
}
