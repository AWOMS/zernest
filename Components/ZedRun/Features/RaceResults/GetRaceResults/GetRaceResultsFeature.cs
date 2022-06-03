using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AWOMS.Zernest.Components.ZedRun.GraphQL;
using System.Dynamic;
using System.Text.Json;

namespace AWOMS.Zernest.Components.ZedRun.Features.RaceResults.GetRaceResults;

public class GetRaceResultsFeature
{
    IMapper _mapper;
    ZedRunGraphQLService _zedRunGraphQLService;
    public GetRaceResultsFeature(IMapper mapper, ZedRunGraphQLService zedRunGraphQLService)
    {
        _mapper = mapper;
        _zedRunGraphQLService = zedRunGraphQLService;
    }

    // get race results uses graphql
    // https://docs.zed.run/racing/getraceresults
    public async Task<AWOMS.Zernest.Components.ZedRun.Features.RaceResults.GetRaceResults.GetRaceResults> GetRaceResults(int first, string after = "",
        bool onlyMyRacehorses = false, int horseId = 0)
    {
        // from the documentation:
        // This endpoint allows you to get race results filtered by:
        // • location (country, city)
        // • dates (from, to) -- (format is wrong here too)
        // • distance (from, to)
        // • classes
        // • boolean flag onlyMyRacehorses

        // KEEP: this is an example of how to send time in the proper UTC format:
        // {
        // dates = new
        // {
        //     from = new DateTime(2022, 4, 21, 12, 0, 0, DateTimeKind.Utc),
        //     to = new DateTime(2022, 4, 28, 12, 0, 0, DateTimeKind.Utc),
        // }

        // from their live site (dates format is different too):
        // - horses

        dynamic filters = new ExpandoObject();
        filters.first = first;

        if (!string.IsNullOrEmpty(after))
        {
            filters.after = after;
        }

        dynamic input = new ExpandoObject();
        input.onlyMyRacehorses = onlyMyRacehorses;
        filters.input = input;

        if (horseId > 0)
        {
            filters.input.horses = new int[] {
                horseId,
            };
        }

        var results = await _zedRunGraphQLService
            .RunQuery<Data>(
                GraphQLQuery.QueryText, filters);
        
        Console.WriteLine(JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true }));
        return results.GetRaceResults;
    }
}
