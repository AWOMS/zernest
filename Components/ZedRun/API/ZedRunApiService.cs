using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWOMS.Zernest.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Text.Json;

namespace AWOMS.Zernest.Components.ZedRun.API;

public class ZedRunApiService
{
    ILogger<ZedRunApiService> _logger { get; }
    Settings _settings { get; }
    HttpClient _httpClient { get; }
    IMemoryCache _memoryCache { get; }

    public ZedRunApiService(ILogger<ZedRunApiService> logger,
        IConfiguration configuration, HttpClient httpClient, IMemoryCache memoryCache)
    {
        _logger = logger;
        _settings = configuration.GetRequiredSection("Settings").Get<Settings>();
        _httpClient = httpClient;
        _httpClient.AddUserAgent(_settings.ApplicationName, _settings.ApplicationVersion, _settings.ApplicationUrl);
        _memoryCache = memoryCache;
    }

    public async Task<T> MakeApiCall<T>(string url)
    {
        //we cant make the api call if we aren't appearing as our allowed IP
        //or we risk getting blocked/banned
        // @TODO we only need to check this on startup
        var pass = await _memoryCache.GetOrCreateAsync("ipcheck", async e =>
        {
            e.SetOptions(new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

            if (_settings.EnforceOutboundIpCheck
                    && !await HttpHelpers.IsOutboundIpAddressAllowed(_httpClient, _settings.OutboundIpSource, _settings.AllowedOutboundIpAddresses))
            {
                throw new UnauthorizedAccessException("Outbound IP address does not pass allowed list check.");
            }
            _logger.LogInformation($"IPCheck: PASS (1 hour cache)");
            return true;
        });

        _logger.LogDebug($"Getting URL: {url}");
        var response = await _httpClient.GetAsync(url);
        _logger.LogDebug($"Response status: ({(int)response.StatusCode}) {response.StatusCode}");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        if (responseBody == null) return default(T);
        return JsonSerializer.Deserialize<T>(responseBody);
    }
}