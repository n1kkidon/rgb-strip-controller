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
        public IActionResult GetIPv4Address()
        {
            return Ok(HttpService.GetRemoteIp(HttpContext));
        }
    }
}
