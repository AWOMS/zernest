using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AWOMS.Zernest.Components.ZedRun;
public static class HttpHelpers
{
    public static async Task<string> GetOutboundIpAddress(HttpClient httpClient, string getIpServiceUrl)
    {
        var response = await httpClient.GetAsync(getIpServiceUrl);
        response.EnsureSuccessStatusCode();
        var html = await response.Content.ReadAsStringAsync();
        var ip = html.Trim();
        Console.WriteLine($"Outbound IP address detected as: {ip}");
        return ip;
    }

    public static async Task<bool> IsOutboundIpAddressAllowed(HttpClient httpClient, string getIpServiceUrl, string[] allowedOutboundIpAddresses)
    {
        //todo: only do this on startup/cache results
        var outboundIpAddress = await GetOutboundIpAddress(httpClient, getIpServiceUrl);
        return allowedOutboundIpAddresses.Any(x => x == outboundIpAddress);
    }
}
