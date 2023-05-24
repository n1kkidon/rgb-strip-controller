using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        var Ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        if(Ip == "::1")
            Ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();

        
        return Ok(Ip);
    }
}

