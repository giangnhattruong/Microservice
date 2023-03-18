using Microsoft.AspNetCore.Mvc;
using UserService.Extensions;
using UserService.Resources;

namespace UserService.Controllers.Config;

public static class InvalidModelStateResponseFactory
{
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var errors = context.ModelState.GetErrorMessages();
        var response = new ErrorResource(messages: errors);

        return new BadRequestObjectResult(response);
    }
}