using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiServices.Services;

namespace ApiServices.Controllers;

[ApiController]
[Route("rgb")]
public class RgbController : ControllerBase
{
    private HttpService _http;
    public RgbController(HttpService http) => _http = http;

    [HttpGet("getLedState")]
    public bool GetLedStatus()
    {
        return true;
    }
    [HttpGet("getPermissionState")]
    public IActionResult GetPermissionState()
    {
        var ip = HttpService.GetRemoteIp(HttpContext);
        var resp = false;

        var comparison = _http.LocalGateway?.ToString().TrimEnd("1234567890".ToCharArray());
        if (ip != null && ip.Equals(Dns.GetHostEntry("nikkidon.org")))
            resp = true;
        else if (comparison != null && ip != null && ip.StartsWith(comparison))
            resp = true;

        Console.WriteLine(comparison);
        Console.WriteLine(ip);
        return Ok(resp);
    }


}

