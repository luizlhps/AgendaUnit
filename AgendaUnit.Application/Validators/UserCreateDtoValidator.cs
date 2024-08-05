using AgendaUnit.Application.DTO.AuthenticationManagerDto;
using AgendaUnit.Application.DTO.UserDto;
using AgendaUnit.Domain.Models;
using FluentValidation;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x => x.Username).NotNull().MinimumLength(6);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.RoleId).NotNull();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().Length(11);
    }
}