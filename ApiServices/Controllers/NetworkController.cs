using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiServices.Services;

namespace ApiServices.Controllers
{
    [ApiController]
    [Route("network")]
    public class NetworkController : ControllerBase
    {
        [HttpGet("getIPv4Address")]
        public string GetIPv4Address() => HttpService.GetRemoteIp(HttpContext)+"";
    }
}
