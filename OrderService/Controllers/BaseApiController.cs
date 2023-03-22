using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers;

[Route("/api/v1.0/[controller]")]
[Produces("application/json")]
[ApiController]
[Authorize]
public abstract class BaseApiController : ControllerBase
{
} 