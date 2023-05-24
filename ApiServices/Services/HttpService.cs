using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ApiServices.Services
{
    public class HttpService
    {
        private HttpClient Client { get; set; }
        public IPAddress? LocalGateway { get; set; }
        public IPAddress? LocalIPv4 { get; set; }
        public PhysicalAddress? LocalMAC { get; set; }
        public HttpService() 
        {
            Client = new HttpClient();

            var pattern = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            var nic = NetworkInterface.GetAllNetworkInterfaces()
                .Where(x => x.GetIPProperties().GatewayAddresses.Count > 0)
                .Where(x => x.GetIPProperties().GatewayAddresses.FirstOrDefault(x => pattern.IsMatch(x.Address.ToString())) != null)
                .ToList().FirstOrDefault();
            LocalGateway = nic?.GetIPProperties().GatewayAddresses.FirstOrDefault(x => pattern.IsMatch(x.Address.ToString()))?.Address;
            LocalMAC = nic?.GetPhysicalAddress();
            LocalIPv4 = nic?.GetIPProperties().UnicastAddresses.Where(x => x.IPv4Mask.ToString() != "0.0.0.0").FirstOrDefault()?.Address;
        }
        public async Task<HttpResponseMessage> Get(string endpointAddress, string? jwtToken = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpointAddress);
            if (jwtToken != null)
                request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            return await Client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> Post(string endpointAddress, object serializableItem, string? jwtToken = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpointAddress);
            return await BaseMethod(request, serializableItem, jwtToken);
        }
        public async Task<HttpResponseMessage> Patch(string endpointAddress, object serializableItem, string? jwtToken = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, endpointAddress);
            return await BaseMethod(request, serializableItem, jwtToken);
        }
        public async Task<HttpResponseMessage> Put(string endpointAddress, object serializableItem, string? jwtToken = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, endpointAddress);
            return await BaseMethod(request, serializableItem, jwtToken);
        }


        private async Task<HttpResponseMessage> BaseMethod(HttpRequestMessage request, object serializableItem, string? jwt)
        {
            var json = JsonConvert.SerializeObject(serializableItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;
            if (jwt != null)
                request.Headers.Add("Authorization", $"Bearer {jwt}");
            return await Client.SendAsync(request);
        }

        public static string? GetRemoteIp(HttpContext context)
        {
            var Ip = context.Connection.RemoteIpAddress?.ToString();
            if (Ip == "::1")
                Ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
            return Ip;
        }
    }
}
