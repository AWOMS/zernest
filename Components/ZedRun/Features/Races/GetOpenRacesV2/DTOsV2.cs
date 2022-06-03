using System.Text.Json.Serialization;

namespace AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRacesV2;

public record Data(
    [property: JsonPropertyName("races")] Races Races
);

public record Edge(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("cursor")] string Cursor,
    [property: JsonPropertyName("node")] Node Node
);

public record EventType(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("labels")] IReadOnlyList<Label> Labels,
    [property: JsonPropertyName("title")] string Title
);

public record Horse(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("bloodline")] string Bloodline,
    [property: JsonPropertyName("breedType")] string BreedType,
    [property: JsonPropertyName("career")] string Career,
    [property: JsonPropertyName("class")] int Class,
    [property: JsonPropertyName("coat")] string Coat,
    [property: JsonPropertyName("gate")] string Gate,
    [property: JsonPropertyName("gender")] string Gender,
    [property: JsonPropertyName("genotype")] string Genotype,
    [property: JsonPropertyName("hexCode")] string HexCode,
    [property: JsonPropertyName("horseId")] int HorseId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("owner")] string Owner,
    [property: JsonPropertyName("races")] int Races,
    [property: JsonPropertyName("rating")] double Rating,
    [property: JsonPropertyName("skin")] Skin Skin,
    [property: JsonPropertyName("stable")] string Stable,
    [property: JsonPropertyName("winRate")] double WinRate
);

public record Label(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("name")] string Name
);

public record Node(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("class")] int Class,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("countryCode")] string CountryCode,
    [property: JsonPropertyName("eventType")] EventType EventType,
    [property: JsonPropertyName("fee")] string Fee,
    [property: JsonPropertyName("horses")] IReadOnlyList<Horse> Horses,
    [property: JsonPropertyName("length")] int Length,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("prize")] Prize Prize,
    [property: JsonPropertyName("prizePool")] PrizePool PrizePool,
    [property: JsonPropertyName("raceFactor")] RaceFactor RaceFactor,
    [property: JsonPropertyName("raceId")] string RaceId,
    [property: JsonPropertyName("startTime")] DateTime? StartTime,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("weather")] string Weather
);

public record PageInfo(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("endCursor")] string EndCursor,
    [property: JsonPropertyName("hasNextPage")] bool HasNextPage,
    [property: JsonPropertyName("hasPreviousPage")] bool HasPreviousPage,
    [property: JsonPropertyName("startCursor")] string StartCursor
);

public record Prize(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("first")] string First,
    [property: JsonPropertyName("second")] string Second,
    [property: JsonPropertyName("third")] string Third,
    [property: JsonPropertyName("total")] string Total
);

public record PrizePool(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("first")] string First,
    [property: JsonPropertyName("second")] string Second,
    [property: JsonPropertyName("third")] string Third,
    [property: JsonPropertyName("total")] string Total
);

public record RaceFactor(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("surfaceValue")] string SurfaceValue
);

public record Races(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("edges")] IReadOnlyList<Edge> Edges,
    [property: JsonPropertyName("pageInfo")] PageInfo PageInfo
);

public record Root(
    [property: JsonPropertyName("data")] Data Data
);

public record Skin(
    [property: JsonPropertyName("__typename")] string Typename,
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("texture")] string Texture
);

