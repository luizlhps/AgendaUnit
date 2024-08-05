using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Domain.Models;
using FluentValidation;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Username).NotNull().MinimumLength(6);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}