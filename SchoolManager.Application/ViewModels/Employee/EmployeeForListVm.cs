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
        //position
        public int PositionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SchoolManager.Domain.Model.Employee, EmployeeForListVm>();
                //Jeśli te same parametry w tabeli, to poniższy kod jest niepotrzebny
                //.ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.Position));
                //.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
        }
    }
}
