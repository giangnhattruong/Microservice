using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Domain.Controllers;

[Route("/api/v1.0/[controller]")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase
{
}