using FluentValidation;
using SchoolManager.Application.ViewModels.Position;
using System.Collections.Generic;

namespace SchoolManager.Application.ViewModels.Position
{
    public class AddPositionViewModel : NewPositionVm
    {
        public List<Position> Positions { get; set; } = new List<Position>();
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