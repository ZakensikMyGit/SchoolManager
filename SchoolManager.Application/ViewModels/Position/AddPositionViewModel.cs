using FluentValidation;
using SchoolManager.Application.ViewModels.Position;
using System.Collections.Generic;
using DomainPosition = SchoolManager.Domain.Model.Position;

namespace SchoolManager.Application.ViewModels.Position
{
    public class AddPositionViewModel : NewPositionVm
    {
        public List<DomainPosition> Positions { get; set; } = new List<DomainPosition>();
    }
}

public class AddPositionViewModelValidator : AbstractValidator<AddPositionViewModel>
{
    public AddPositionViewModelValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotEmpty().WithMessage("Uzupełnij pole");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Uzupełnij pole");
    }
}