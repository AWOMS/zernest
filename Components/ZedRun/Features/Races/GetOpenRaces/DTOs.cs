using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AWOMS.Zernest.Components.ZedRun.Features.Races.GetOpenRaces;

public record GatesDTO([property: JsonPropertyName("2")] _2DTO _2, [property: JsonPropertyName("9")] _9DTO _9, [property: JsonPropertyName("10")] _10DTO _10, [property: JsonPropertyName("1")] _1DTO _1, [property: JsonPropertyName("3")] _3DTO _3, [property: JsonPropertyName("4")] _4DTO _4, [property: JsonPropertyName("12")] _12DTO _12, [property: JsonPropertyName("8")] _8DTO _8, [property: JsonPropertyName("5")] _5DTO _5, [property: JsonPropertyName("6")] _6DTO _6, [property: JsonPropertyName("7")] _7DTO _7, [property: JsonPropertyName("11")] _11DTO _11);
public record OpenRaceDTO([property: JsonPropertyName("city")] string City, [property: JsonPropertyName("class")] int Class, [property: JsonPropertyName("country")] string Country, [property: JsonPropertyName("country_code")] string CountryCode, [property: JsonPropertyName("event_type")] string EventType, [property: JsonPropertyName("extended_timeout_at")] object ExtendedTimeoutAt, [property: JsonPropertyName("fee")] string Fee, [property: JsonPropertyName("final_positions")] object FinalPositions, [property: JsonPropertyName("gates")] GatesDTO Gates, [property: JsonPropertyName("length")] int Length, [property: JsonPropertyName("name")] string Name, [property: JsonPropertyName("prize")] PrizeDTO Prize, [property: JsonPropertyName("race_factor")] RaceFactorDTO RaceFactor, [property: JsonPropertyName("race_id")] string RaceId, [property: JsonPropertyName("racetrack_code")] string RacetrackCode, [property: JsonPropertyName("rules")] IReadOnlyList<RuleDTO> Rules, [property: JsonPropertyName("sponsors")] IReadOnlyList<object> Sponsors, [property: JsonPropertyName("start_time")] DateTime StartTime, [property: JsonPropertyName("status")] string Status, [property: JsonPropertyName("timeout_at")] object TimeoutAt, [property: JsonPropertyName("weather")] string Weather);
public record PrizeDTO([property: JsonPropertyName("first")] string First, [property: JsonPropertyName("second")] string Second, [property: JsonPropertyName("third")] string Third, [property: JsonPropertyName("total")] string Total);
public record RaceFactorDTO([property: JsonPropertyName("surface_value")] string SurfaceValue);
public record RuleDTO([property: JsonPropertyName("description")] string Description, [property: JsonPropertyName("title")] string Title);
public record _1DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _2DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _3DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _4DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _5DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _6DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _7DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _8DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _9DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _10DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _11DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);
public record _12DTO([property: JsonPropertyName("horse_id")] int HorseId, [property: JsonPropertyName("status")] string Status);