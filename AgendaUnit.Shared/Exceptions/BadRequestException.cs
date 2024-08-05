using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgendaUnit.Shared.Exceptions;

public class BadRequestException : BaseException
{
    public ModelStateDictionary ModelState { get; }

    public BadRequestException(ModelStateDictionary modelState, string message) : base(message)
    {
        StatusCode = 400;
        ModelState = modelState;
    }
}