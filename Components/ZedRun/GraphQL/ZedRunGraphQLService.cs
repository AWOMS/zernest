using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Text.Json;
using GraphQL.Client.Abstractions;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;
using AWOMS.Zernest.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace AWOMS.Zernest.Components.ZedRun.GraphQL;

public class ZedRunGraphQLService
{
    private const string ZedRunDateTimeUtcFormat = "yyyy-MM-ddTHH:mm:ss.fffK";
    private readonly IGraphQLClient _graphQLClient;
    ILogger<ZedRunGraphQLService> _logger { get; }
    Settings _settings { get; }
    HttpClient _httpClient { get; }
    IMemoryCache _memoryCache { get; }

    public ZedRunGraphQLService(ILogger<ZedRunGraphQLService> logger,
        IConfiguration configuration, HttpClient httpClient, IMemoryCache memoryCache)
    {
        _logger = logger;
        _settings = configuration.GetRequiredSection("Settings").Get<Settings>();
        _httpClient = httpClient;
        _memoryCache = memoryCache;

        _httpClient.BaseAddress = new Uri(_settings.ZedGraphQLUrl);
        _httpClient.AddUserAgent(_settings.ApplicationName, _settings.ApplicationVersion, _settings.ApplicationUrl);

        var opt = new GraphQLHttpClientOptions();
        opt.EndPoint = new Uri(_settings.ZedGraphQLUrl);

        // remove "T" from date, add "Z" to indicate UTC
        var jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
        jsonSerializerSettings.DateFormatString = ZedRunDateTimeUtcFormat;
        var serializationOptions = new NewtonsoftJsonSerializer(jsonSerializerSettings);

        _graphQLClient = new GraphQLHttpClient(opt, serializationOptions, _httpClient);
    }

    // formats query + filter values to GraphQL and executes
    // returning expected data type (.Data is from the library)
    public async Task<T> RunQuery<T>(string query, object? variables = null)
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

        var queryResults = new GraphQLRequest
        {
            Query = query,
        };

        if (variables != null)
        {
            queryResults.Variables = variables;
        }

        // write request to logs for debugging, using newtonsoft
        var requestJsonOptions = new Newtonsoft.Json.JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            DateFormatString = ZedRunDateTimeUtcFormat
        };
        var requestJsonSerializer = new NewtonsoftJsonSerializer(requestJsonOptions);
        _logger.LogDebug(requestJsonSerializer.SerializeToString(queryResults));

        var response = await _graphQLClient.SendQueryAsync<T>(queryResults);
        if (response.Errors != null && response.Errors.Any())
        {
            _logger.LogDebug("Errors detected in response.");
            // write response to logs for debugging, custom options to not escape unicode
            var responseJsonOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(response, responseJsonOptions);
            _logger.LogDebug(jsonString);
        }

        return response.Data;
    }
}
