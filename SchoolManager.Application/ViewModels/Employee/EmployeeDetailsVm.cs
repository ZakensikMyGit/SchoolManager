using AutoMapper;
using SchoolManager.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Employee
{
    public class EmployeeDetailsVm : IMapFrom<SchoolManager.Domain.Model.Employee>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string GroupName { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string PhoneNumber { get; set; }
       // public string Email { get; set; }
        public int PensumHours { get; set; }
        public double WorkingHours { get; set; }
        public double ActualWorkingHours => PensumHours * WorkingHours;
        public bool IsDirector { get; set; }
        public string Education { get; set; }


        //public List<ListChildForListVm> Children { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SchoolManager.Domain.Model.Employee, EmployeeDetailsVm>()
               .ForMember(dest => dest.PositionName,
                    opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : string.Empty))
                .ForMember(s => s.DateOfEmployment, opt => opt.MapFrom(src => src.EmploymentDate));
            //.ForMember(s => s.DateOfBirth, opt => opt.Ignore()) //jeśli nie chcemy mapować tego pola, np. gdy nie ma go w bazie danych
            //.ForMember(s => s.PhoneNumber, opt => opt.Ignore()) //jeśli nie chcemy mapować tego pola, np. gdy nie ma go w bazie danych
            //.ForMember(s => s.Email, opt => opt.Ignore()) //jeśli nie chcemy mapować tego pola, np. gdy nie ma go w bazie danych
        }
    }
}
