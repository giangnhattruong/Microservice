using Microsoft.AspNetCore.Mvc;
using ProductService.Extensions;
using ProductService.Resources;

namespace ProductService.Domain.Controllers.Config;

public static class InvalidModelStateResponseFactory
{
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var errors = context.ModelState.GetErrorMessages();
        var response = new ErrorResource(messages: errors);

        return new BadRequestObjectResult(response);
    }
}