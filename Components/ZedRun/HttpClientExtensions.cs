using System.Net.Http.Headers;
using System.Net.Http;
namespace AWOMS.Zernest.Components.ZedRun;
public static class HttpClientExtensions
{
    //calls to zedrun API will be rejected with 403 without user-agent
    //this sets user agent to our application name/version
    public static void AddUserAgent(this HttpClient client, string applicationName, string applicationVersion, string applicationUrl)
    {
        var productValue = new ProductInfoHeaderValue(applicationName, applicationVersion);
        var commentValue = new ProductInfoHeaderValue($"(+{applicationUrl})");
        client.DefaultRequestHeaders.UserAgent.Add(productValue);
        client.DefaultRequestHeaders.UserAgent.Add(commentValue);
    }
}