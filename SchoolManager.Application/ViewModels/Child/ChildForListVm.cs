using SchoolManager.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Child
{
    public class ChildForListVm : IMapFrom<SchoolManager.Domain.Model.Child>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int? TeacherId { get; set; }
        public string TeacherName { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<SchoolManager.Domain.Model.Child, ChildForListVm>()
                .ForMember(s => s.TeacherId,
                           opt => opt.MapFrom(src => src.Group.Teacher != null ? (int?)src.Group.Teacher.Id : null))
                .ForMember(s => s.GroupName,
                           opt => opt.MapFrom(src => src.Group.GroupName))
                .ForMember(s => s.TeacherName,
                           opt => opt.MapFrom(src => src.Group.Teacher.FullName));
        }
    }
}


