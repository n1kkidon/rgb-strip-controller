using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiServices.Services;

namespace ApiServices.Controllers;

[ApiController]
[Route("rgb")]
public class RgbController : ControllerBase
{
    private readonly HttpService _http;
    private readonly RGB _rgb;

    public RgbController(HttpService http, RGB rgb)
    {
        _http = http;
        _rgb = rgb;
    }

    [HttpGet("getLedState")]
    public bool GetLedStatus() => RGB.LED_IS_ON;

    [HttpGet("getCurrentLedColor")]
    public string GetCurrentLedColor() => _rgb.GetHex();

    [HttpGet("getPermissionState")]
    public bool GetPermissionState() => CheckIfIpIsLocalNetwork(HttpContext, _http);


    public static bool CheckIfIpIsLocalNetwork(HttpContext context, HttpService http)
    {
        var ip = HttpService.GetRemoteIp(context);
        var resp = false;

        if (ip == null)
            return resp;

        var comparison = http.LocalGateway?.ToString().TrimEnd("1234567890".ToCharArray());
        if (ip.Equals(Dns.GetHostEntry("nikkidon.org").AddressList.FirstOrDefault()?.ToString()))
            resp = true;
        else if (comparison != null && ip.StartsWith(comparison))
            resp = true;
        else if (ip.Equals("127.0.0.1"))
            resp = true;
        return resp;
    }


}

