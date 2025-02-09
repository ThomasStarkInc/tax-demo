using Application.Municipalities.CreateMunicipality;

using FluentValidation;

namespace Application.Todos.Create;

public class CreateMunicipalityCommandValidator : AbstractValidator<CreateMunicipalityCommand>
{
    public CreateMunicipalityCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(2).MaximumLength(100);
    }
}
