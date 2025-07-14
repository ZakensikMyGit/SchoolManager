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
    public class ScheduleEntryVm : IMapFrom<ScheduleEntry>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ScheduleEntryTypeEnum EntryType { get; set; }
        public string Description { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ScheduleEntry, ScheduleEntryVm>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.GroupName));
        }
    }
}