using AgendaUnit.Shared.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

public class ValidateModelAttribute : ActionFilterAttribute
{
    private readonly IServiceProvider _serviceProvider;

    public ValidateModelAttribute(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments)
        {
            var argumentType = argument.Value.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
            var validator = _serviceProvider.GetService(validatorType);

            if (validator != null)
            {
                var validationContext = new ValidationContext<object>(argument.Value);
                var validationResult = ((IValidator)validator).Validate(validationContext);

                if (!validationResult.IsValid)
                {
                    var modelState = context.ModelState;
                    foreach (var error in validationResult.Errors)
                    {
                        modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }

                    throw new BadRequestException(modelState, "error de validação");
                }
            }
        }

        base.OnActionExecuting(context);
    }
}
