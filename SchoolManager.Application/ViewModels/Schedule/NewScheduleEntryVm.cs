using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class NewScheduleEntryVm : IMapFrom<ScheduleEntry>
    {
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public int GroupId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime RangeStart { get; set; } = DateTime.Today;
        public DateTime RangeEnd { get; set; } = DateTime.Today;
        public ScheduleEntryTypeEnum EntryType { get; set; }
        public string Description { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<NewScheduleEntryVm, ScheduleEntry>()
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.Position, opt => opt.Ignore())
                .ForMember(dest => dest.Group, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}