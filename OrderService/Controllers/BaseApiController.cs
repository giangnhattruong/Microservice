using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers;

[Route("/api/v1.0/[controller]")]
[Produces("application/json")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
} 