using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWOMS.Zernest.Components.ZedRun.API;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses;

// https://api.zed.run/api/v1/horses/get_user_horses?public_address=0xfD0De0CAd2A2Ef78654681b5648e28250c814C7D&offset=0&gen[]=1&gen[]=268&horse_name=&sort_by=created_by_desc
// api call returns max of 10 horses at a time
// if returns 10, increment offset and repeat
// can specify horse_name
public class GetStableHorsesFeature
{
    ILogger<GetStableHorsesFeature> _logger { get; }
    IMemoryCache _memoryCache { get; }
    ZedRunApiService _zedRunApiService;
    public GetStableHorsesFeature(ILogger<GetStableHorsesFeature> logger,
        IMemoryCache memoryCache, ZedRunApiService zedRunApiService)
    {
        _logger = logger;
        _memoryCache = memoryCache;
        _zedRunApiService = zedRunApiService;
    }

    public async Task<StableHorseDTO> GetSingleStableHorses(string stableAddress, string horseName)
    {
        if (string.IsNullOrWhiteSpace(stableAddress)) throw new ArgumentNullException(nameof(stableAddress));
        if (string.IsNullOrWhiteSpace(horseName)) throw new ArgumentNullException(nameof(horseName));

        var cacheKey = $"GetStableHorses-{stableAddress}-{horseName}";
        var cacheTimespan = TimeSpan.FromSeconds(60);

        if (_memoryCache.TryGetValue(cacheKey, out object? value)
             && value is StableHorseDTO horse)
        {
            _logger.LogDebug($"Cached '{cacheKey}' is still in cache, returning results");
            return horse;
        }

        _logger.LogDebug($"Cached '{cacheKey}' is expired, making API call to get new data");

        var url = $"https://api.zed.run/api/v1/horses/get_user_horses?public_address={stableAddress}&horse_name={horseName}";
        var list = await _zedRunApiService.MakeApiCall<IEnumerable<StableHorseDTO>>(url);
        horse = list.First();

        _logger.LogDebug($"Caching '{cacheKey}' and returning result for {cacheTimespan} time");
        return _memoryCache.Set<StableHorseDTO>(cacheKey, horse,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheTimespan
            });
    }

    // loops through and gets ALL horses belonging to a stable address
    public async Task<IEnumerable<StableHorseDTO>> GetStableHorses(string stableAddress)
    {
        if (string.IsNullOrWhiteSpace(stableAddress)) throw new ArgumentNullException(nameof(stableAddress));

        var cacheKey = $"GetStableHorses-{stableAddress}";
        var cacheTimespan = TimeSpan.FromSeconds(60);

        if (_memoryCache.TryGetValue(cacheKey, out object? value)
             && value is IEnumerable<StableHorseDTO> results)
        {
            _logger.LogDebug($"Cached '{cacheKey}' is still in cache, returning {results.Count()} results");
            return results;
        }

        _logger.LogDebug($"Cached '{cacheKey}' is expired, making API call to get new data");
        var horses = new List<StableHorseDTO>();
        bool hasMore = true;
        var offset = 0;
        while (hasMore)
        {
            var url = $"https://api.zed.run/api/v1/horses/get_user_horses?public_address={stableAddress}&offset={offset}&gen[]=1&gen[]=268&horse_name=&sort_by=created_by_desc";
            results = await _zedRunApiService.MakeApiCall<IEnumerable<StableHorseDTO>>(url);
            horses.AddRange(results);
            _logger.LogDebug($"Adding {results.Count()} horses to results list");
            if (results.Count() == 10)
            {
                offset++;
            }
            else
            {
                hasMore = false;
            }
        }

        _logger.LogDebug($"Caching '{cacheKey}' and returning {horses.Count()} results for {cacheTimespan} time");
        return _memoryCache.Set<IEnumerable<StableHorseDTO>>(cacheKey, horses,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheTimespan
            });
    }
}