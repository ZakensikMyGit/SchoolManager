using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManager.Application.Mapping;
using SchoolManager.Domain.Enums;
using SchoolManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Employee
{
    public class NewEmployeeVm : IMapFrom<SchoolManager.Domain.Model.Employee>, IMapFrom<SchoolManager.Domain.Model.Teacher>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double WorkingHours { get; set; } = 1;
        public DateTime EmploymentDate { get; set; } = DateTime.UtcNow;
        public decimal? BaseSalary { get; set; }

        public int? PositionId { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }

        public GroupEnum? Group { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }
        public string? Education { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewEmployeeVm, SchoolManager.Domain.Model.Employee>()
                 .ForMember(dest => dest.Position, opt => opt.Ignore())
                .ReverseMap();

            profile.CreateMap<NewEmployeeVm, SchoolManager.Domain.Model.Teacher>()
                .ForMember(dest => dest.Position, opt => opt.Ignore())
                .ReverseMap();
        }
    }

    public class NewEmployeeVmValidator : AbstractValidator<NewEmployeeVm>
    {
        public NewEmployeeVmValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Uzupejnij pole");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Uzupejnij pole");
            RuleFor(x => x.EmploymentDate).NotEmpty().WithMessage("Uzupejnij pole");
            RuleFor(x => x.WorkingHours).GreaterThan(0).LessThanOrEqualTo(2).WithMessage("Godziny pracy powinny być większe niż 0 i mniejsze lub równe 2");
        }
    }
}
