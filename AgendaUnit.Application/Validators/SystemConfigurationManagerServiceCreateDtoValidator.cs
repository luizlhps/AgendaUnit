
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using FluentValidation;

public class SystemConfigurationManagerServiceCreateDtoValidator : AbstractValidator<SystemConfigurationManagerServiceCreateDto>
{
    public SystemConfigurationManagerServiceCreateDtoValidator()
    {
        RuleFor(x => x.Service.Name).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Service.Price).GreaterThan(0);
        RuleFor(x => x.Success).NotNull();
        RuleFor(x => x.Service.Duration).GreaterThan(new TimeSpan(0, 0, 0)).WithMessage("A duração deve ser maior que zero.");
    }
}
