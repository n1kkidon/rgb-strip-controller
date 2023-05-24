using Microsoft.AspNetCore.Mvc;
namespace ApiServices.Controllers;

[ApiController]
[Route("rgb")]
public class RgbController : ControllerBase
{
    [HttpGet("getLedState")]
    public bool GetLedStatus()
    {
        return true;
    }
    [HttpGet("getPermissionState")]
    public IActionResult GetPermissionState()
    {
        var Ip = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        var localIp = HttpContext.Connection.LocalIpAddress?.MapToIPv4().ToString();
        return Ok($"remote: {Ip}\nlocal: {localIp}");
    }
}

