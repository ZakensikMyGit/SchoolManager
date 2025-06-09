using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManager.Application.Mapping;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Employee
{
    public class NewEmployeeVm : IMapFrom<SchoolManager.Domain.Model.Employee>
    {
      public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int PositionId { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }

        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<NewEmployeeVm, SchoolManager.Domain.Model.Employee>();
        //}
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewEmployeeVm, SchoolManager.Domain.Model.Employee>()
                .ForMember(dest => dest.Position, opt => opt.Ignore()); 


    }
    }
}
