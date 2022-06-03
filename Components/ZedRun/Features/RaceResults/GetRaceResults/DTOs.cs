using System.Text.Json.Serialization;

namespace AWOMS.Zernest.Components.ZedRun.Features.RaceResults.GetRaceResults;

public record Data(
    [property: JsonPropertyName("getRaceResults")] GetRaceResults GetRaceResults
);

public record Edge(
    [property: JsonPropertyName("cursor")] string Cursor,
    [property: JsonPropertyName("node")] Node Node
);

public record GetRaceResults(
    [property: JsonPropertyName("edges")] IReadOnlyList<Edge> Edges,
    [property: JsonPropertyName("pageInfo")] PageInfo PageInfo
);

public record Horse(
    [property: JsonPropertyName("bloodline")] string Bloodline,
    [property: JsonPropertyName("breedType")] string BreedType,
    [property: JsonPropertyName("class")] int Class,
    [property: JsonPropertyName("coat")] string Coat,
    [property: JsonPropertyName("finalPosition")] string FinalPosition,
    [property: JsonPropertyName("finishTime")] double FinishTime,
    [property: JsonPropertyName("gate")] string Gate,
    [property: JsonPropertyName("gen")] string Gen,
    [property: JsonPropertyName("gender")] string Gender,
    [property: JsonPropertyName("hexColor")] string HexColor,
    [property: JsonPropertyName("horseId")] int HorseId,
    [property: JsonPropertyName("imgUrl")] string ImgUrl,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("ownerAddress")] string OwnerAddress,
    [property: JsonPropertyName("stableName")] string StableName
);

public record Node(
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("class")] int Class,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("fee")] string Fee,
    [property: JsonPropertyName("horses")] IReadOnlyList<Horse> Horses,
    [property: JsonPropertyName("length")] int Length,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("prizePool")] PrizePool PrizePool,
    [property: JsonPropertyName("raceId")] string RaceId,
    [property: JsonPropertyName("startTime")] DateTime StartTime,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("weather")] string Weather
);

public record PageInfo(
    [property: JsonPropertyName("endCursor")] string EndCursor,
    [property: JsonPropertyName("hasNextPage")] bool HasNextPage,
    [property: JsonPropertyName("hasPreviousPage")] bool HasPreviousPage,
    [property: JsonPropertyName("startCursor")] string StartCursor
);

public record PrizePool(
    [property: JsonPropertyName("first")] string First,
    [property: JsonPropertyName("second")] string Second,
    [property: JsonPropertyName("third")] string Third
);

public record Root(
    [property: JsonPropertyName("data")] Data Data
);
