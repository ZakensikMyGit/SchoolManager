using SchoolManager.Application.Mapping;
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Schedule
{
    public class EditScheduleEntryVm : IMapFrom <ScheduleEntry>
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ScheduleEntryTypeEnum EntryType { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public GroupEnum Group { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ScheduleEntry, EditScheduleEntryVm>().ReverseMap();
        }
    }
}