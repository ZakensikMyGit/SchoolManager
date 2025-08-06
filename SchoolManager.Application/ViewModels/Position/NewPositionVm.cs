using AutoMapper;
using FluentValidation;
using SchoolManager.Application.ViewModels.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.ViewModels.Position
{
    public class NewPositionVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPositionVm, Domain.Model.Position>()
                .ForMember(dest => dest.Employees, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}

public class NewPositionVmValidator : AbstractValidator<NewPositionVm>
{
    public NewPositionVmValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty().WithMessage("Uzupełnij pole");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Uzupełnij pole");
    }
}
