using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWOMS.Zernest.Components.ZedRun.API;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory;

public class GetHorsesWithHistoryFeature
{
    ILogger<GetHorsesWithHistoryFeature> _logger { get; }
    IMemoryCache _memoryCache { get; }
    ZedRunApiService _zedRunApiService { get; }
    public GetHorsesWithHistoryFeature(ILogger<GetHorsesWithHistoryFeature> logger,
        IMemoryCache memoryCache, ZedRunApiService zedRunApiService)
    {
        _logger = logger;
        _memoryCache = memoryCache;
        _zedRunApiService = zedRunApiService;
    }

    public async Task<HorseWithHistoryDTO> GetHorseWithHistory(string horseId)
    {
        if (horseId == null) throw new ArgumentNullException(nameof(horseId));
        var list = await GetHorsesWithHistory(new string[] { horseId });
        return list.FirstOrDefault();
    }

    public async Task<IEnumerable<HorseWithHistoryDTO>> GetHorsesWithHistory(string[] horseIds)
    {
        if (horseIds == null) throw new ArgumentNullException(nameof(horseIds));

        var cacheKey = $"GetHorsesWithHistory-{string.Join(",", horseIds)}";
        var cacheTimespan = TimeSpan.FromSeconds(60);

        if (_memoryCache.TryGetValue(cacheKey, out object? value)
             && value is IEnumerable<HorseWithHistoryDTO> results)
        {
            _logger.LogDebug($"Cached '{cacheKey}' is still in cache, returning {results.Count()} results");
            return results;
        }

        _logger.LogDebug($"Cached '{cacheKey}' is expired, making API call to get new data");

        //format: https://api.zed.run/api/v1/horses/get_horses/?horse_ids[]=393368&horse_ids[]=56249&horse_ids[]=209299&horse_ids[]=390676&horse_ids[]=142102&horse_ids[]=155136&horse_ids[]=364394&horse_ids[]=172473&horse_ids[]=13900&horse_ids[]=150213&horse_ids[]=390083
        StringBuilder sb = new StringBuilder("https://api.zed.run/api/v1/horses/get_horses?");
        var prefix = "";
        foreach (var horseId in horseIds)
        {
            sb.Append($"{prefix}horse_ids[]={horseId}");
            if (prefix == "") prefix = "&";
        }
        results = await _zedRunApiService.MakeApiCall<IEnumerable<HorseWithHistoryDTO>>(sb.ToString());

        _logger.LogDebug($"Caching '{cacheKey}' and returning {results.Count()} results for {cacheTimespan} time");
        return _memoryCache.Set<IEnumerable<HorseWithHistoryDTO>>(cacheKey, results,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheTimespan
            });
    }
}