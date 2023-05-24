using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiServices.Services;

namespace ApiServices.Controllers;

[ApiController]
[Route("rgb")]
public class RgbController : ControllerBase
{
    private readonly HttpService _http;
    public RgbController(HttpService http) => _http = http;

    [HttpGet("getLedState")]
    public bool GetLedStatus()
    {
        return RGB.LED_IS_ON;
    }
    [HttpGet("getPermissionState")]
    public IActionResult GetPermissionState()
    {
        var ip = HttpService.GetRemoteIp(HttpContext);
        var resp = false;

        if (ip == null)
            return Ok(resp);

        var comparison = _http.LocalGateway?.ToString().TrimEnd("1234567890".ToCharArray());
        if (ip.Equals(Dns.GetHostEntry("nikkidon.org").AddressList.FirstOrDefault()?.ToString()))
            resp = true;
        else if (comparison != null && ip.StartsWith(comparison))
            resp = true;
        else if (ip.Equals("127.0.0.1"))
            resp = true;
        return Ok(resp);
    }


}

