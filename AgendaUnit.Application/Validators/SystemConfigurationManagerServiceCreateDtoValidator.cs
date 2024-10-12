
using System.Globalization;
using AgendaUnit.Application.DTO.SystemConfigurationManager;
using FluentValidation;
using System.Xml;

public class SystemConfigurationManagerServiceCreateDtoValidator : AbstractValidator<SystemConfigurationManagerServiceCreateDto>
{
    public SystemConfigurationManagerServiceCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Duration)
        .NotEmpty()
        .WithMessage("o campo 'duração é obrigatório'")
        .Must(VerifyIso8601IsValid).WithMessage("Duração inválida");
    }

    private bool VerifyIso8601IsValid(string duration)
    {
        try
        {
            var timeSpan = XmlConvert.ToTimeSpan(duration);
            return timeSpan != TimeSpan.Zero;
        }
        catch (System.Exception)
        {

            return false;
        }


    }

}

