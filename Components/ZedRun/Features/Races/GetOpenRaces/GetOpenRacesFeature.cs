using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AWOMS.Zernest.Components.ZedRun.API;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRaces
{
    public class GetOpenRacesFeature
    {
        ILogger<GetOpenRacesFeature> _logger { get; }
        IMemoryCache _memoryCache { get; }
        ZedRunApiService _zedRunApiService { get; }
        public GetOpenRacesFeature(ILogger<GetOpenRacesFeature> logger,
            IMemoryCache memoryCache, ZedRunApiService zedRunApiService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _zedRunApiService = zedRunApiService;
        }

        public async Task<IEnumerable<OpenRaceDTO>> GetOpenRaces()
        {
            var cacheKey = "GetOpenRaces";
            var cacheTimespan = TimeSpan.FromSeconds(20);

            if (_memoryCache.TryGetValue(cacheKey, out object? value)
                 && value is IEnumerable<OpenRaceDTO> results)
            {
                _logger.LogDebug($"Cached '{cacheKey}' is still in cache, returning {results.Count()} results");
                return results;
            }

            _logger.LogDebug($"Cached '{cacheKey}' is expired, making API call to get new data");
            results = await _zedRunApiService.MakeApiCall<IEnumerable<OpenRaceDTO>>("https://racing-api.zed.run/api/v1/races?status=open");
            _logger.LogDebug($"Caching '{cacheKey}' and returning {results.Count()} results for {cacheTimespan} time");
            return _memoryCache.Set<IEnumerable<OpenRaceDTO>>(cacheKey, results,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheTimespan
                });
        }
    }
}