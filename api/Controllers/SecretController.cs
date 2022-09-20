using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;
[ApiController]
[Route("[controller]")]
public class SecretController:ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult Secret()
    {
        return Ok("ROcke KOROROR 😁😁😁😁😁");

    }
}