using AutoMapper;
using SchoolManager.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Employee
{
    public class EmployeeForListVm : IMapFrom<SchoolManager.Domain.Model.Employee>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PositionId { get; set; }
        public string PositionName { get; set; }
        public decimal? BaseSalary { get; set; }
        public DateTime EmploymentDate { get; set; } = DateTime.UtcNow;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SchoolManager.Domain.Model.Employee, EmployeeForListVm>()
                 .ForMember(dest => dest.PositionName,
                    opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : string.Empty));
        }
    }
}
