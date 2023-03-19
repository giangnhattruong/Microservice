using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers;

[Route("api/v1.0/[controller]")]
[Produces("application/json")]
[ApiController]
public class BaseApiController : ControllerBase
{
}