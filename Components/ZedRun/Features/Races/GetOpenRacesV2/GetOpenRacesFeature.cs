using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AWOMS.Zernest.Components.ZedRun.GraphQL;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRacesV2
{
    public class GetOpenRacesFeature
    {
        ILogger<GetOpenRacesFeature> _logger { get; }
        IMemoryCache _memoryCache { get; }
        ZedRunGraphQLService _zedRunGraphQLService { get; }
        public GetOpenRacesFeature(ILogger<GetOpenRacesFeature> logger,
            IMemoryCache memoryCache, ZedRunGraphQLService zedRunGraphQLService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _zedRunGraphQLService = zedRunGraphQLService;
        }

        // will return all open races (nodes)
        // looping through pages as needed
        public async Task<IEnumerable<Node>> GetOpenRacesV2(bool classAllIsSelected, bool classDiscoveryIsSelected,
            bool classOpenIsSelected, bool class1IsSelected, bool class2IsSelected, bool class3IsSelected,
            bool class4IsSelected, bool class5IsSelected, bool class6IsSelected, bool distanceAllIsSelected,
            bool distance1000IsSelected, bool distance1200IsSelected, bool distance1400IsSelected,
            bool distance1600IsSelected, bool distance1800IsSelected, bool distance2000IsSelected,
            bool distance2200IsSelected, bool distance2400IsSelected, bool distance2600IsSelected,
            bool eventTypeAllIsSelected, bool feeAllIsSelected, bool feeFreeIsSelected,
            bool feePaidIsSelected, bool surfaceAllIsSelected, bool surfaceRigidIsSelected,
            bool surfaceHardIsSelected, bool surfaceYieldingIsSelected, bool surfaceSoftIsSelected,
            bool surfaceOffIsSelected, bool surfaceDirtIsSelected)
        {
            var cacheKey = @$"GetOpenRacesV2-{classAllIsSelected}-{classDiscoveryIsSelected}-{classOpenIsSelected}-{class1IsSelected}-{class2IsSelected}-{class3IsSelected}-{class4IsSelected}-{class5IsSelected}-{class6IsSelected}-{distanceAllIsSelected}-{distance1000IsSelected}-{distance1200IsSelected}-{distance1400IsSelected}-{distance1600IsSelected}-{distance1800IsSelected}-{distance2000IsSelected}-{distance2200IsSelected}-{distance2400IsSelected}-{distance2600IsSelected}-{eventTypeAllIsSelected}-{feeAllIsSelected}-{feeFreeIsSelected}-{feePaidIsSelected}-{surfaceAllIsSelected}-{surfaceRigidIsSelected}-{surfaceHardIsSelected}-{surfaceYieldingIsSelected}-{surfaceSoftIsSelected}-{surfaceOffIsSelected}-{surfaceDirtIsSelected}";

            var cacheTimespan = TimeSpan.FromSeconds(20);

            if (_memoryCache.TryGetValue(cacheKey, out object? value)
                 && value is IEnumerable<Node> results)
            {
                _logger.LogDebug($"Cached '{cacheKey}' is still in cache, returning {results.Count()} results");
                return results;
            }

            _logger.LogDebug($"Cached '{cacheKey}' is expired, making API call to get new data");

            var races = new List<Node>();
            bool hasNextPage = true;
            var first = 11; // 11 to know there's more than 10
            var after = "";
            while (hasNextPage)
            {
                dynamic filters = new ExpandoObject();
                filters.first = first;

                if (!string.IsNullOrEmpty(after))
                {
                    filters.after = after;
                }

                dynamic input = new ExpandoObject();
                input.status = "open";

                // Class Filters - single class at a time
                if (classDiscoveryIsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.Discovery;
                else if (classOpenIsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.Open;
                else if (class1IsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.One;
                else if (class2IsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.Two;
                else if (class3IsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.Three;
                else if (class4IsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.Four;
                else if (class5IsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.Five;
                else if (class6IsSelected)
                    input.@class = Zernest.Models.ZedRun.Classes.Six;

                // Distance Filters
                var distanceFilters = new List<int>();
                if (distance1000IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m1000);
                if (distance1200IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m1200);
                if (distance1400IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m1400);
                if (distance1600IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m1600);
                if (distance1800IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m1800);
                if (distance2000IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m2000);
                if (distance2200IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m2200);
                if (distance2400IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m2400);
                if (distance2600IsSelected)
                    distanceFilters.Add(Zernest.Models.ZedRun.Distances.m2600);
                if (distanceFilters.Any())
                    input.distances = distanceFilters.ToArray();

                // Event Type

                // Fee
                if (feeFreeIsSelected)
                {
                    input.fee = new ExpandoObject();
                    input.fee.min = 0;
                    input.fee.max = 0;
                }
                if (feePaidIsSelected)
                {
                    input.fee = new ExpandoObject();
                    input.fee.min = 0.00000001;
                }

                // Surface Type

                // Query
                filters.input = input;
                Data data = await _zedRunGraphQLService.RunQuery<Data>(Query.Text, filters);
                //_logger.LogDebug(JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
                _logger.LogDebug($"Inner loop found {data.Races.Edges.Count()} with HasNextPage: {data.Races.PageInfo.HasNextPage}");

                foreach (var edge in data.Races.Edges)
                {
                    races.Add(edge.Node);
                }

                if (data.Races.PageInfo.HasNextPage)
                {
                    after = data.Races.PageInfo.EndCursor;


                    hasNextPage = false; // only returns 1st page now
                }
                else
                {
                    hasNextPage = false;
                }
            }

            _logger.LogDebug($"Caching '{cacheKey}' and returning {races.Count()} results for {cacheTimespan} time");

            return _memoryCache.Set<IEnumerable<Node>>(cacheKey, races,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheTimespan
                });
        }
    }
}