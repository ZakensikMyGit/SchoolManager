using SchoolManager.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Child
{
    public class ChildForListVm : IMapFrom<ChildForListVm>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<SchoolManager.Domain.Model.Child, ChildForListVm>()
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.Group.TeacherId));
        }
    }
}


