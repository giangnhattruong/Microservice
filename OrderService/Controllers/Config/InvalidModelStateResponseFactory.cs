using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Extensions;

namespace OrderService.Controllers.Config;

public static class InvalidModelStateResponseFactory
{
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var errors = context.ModelState.GetErrorMessages();
        var response = new ErrorDto(messages: errors);

        return new BadRequestObjectResult(response);
    }
}