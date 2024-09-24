using AgendaUnit.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaUnit.Shared.Utils.ValidatorHelper;

using AgendaUnit.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;


public static class ValidatorHelper
{
    public static void Validate<TInputDto>(TInputDto inputDto, IServiceProvider serviceProvider)
        where TInputDto : class
    {
        var validator = serviceProvider.GetService<IValidator<TInputDto>>();

        if (validator != null)
        {
            var validationResult = validator.Validate(inputDto);

            if (!validationResult.IsValid)
            {
                var ModelState = new ModelStateDictionary();
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                throw new BadRequestException(ModelState, "Erro na validação");
            }
        }
    }
}



