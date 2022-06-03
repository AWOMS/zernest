using System.Text.Json.Serialization;

namespace AWOMS.Zernest.Components.ZedRun.Features.Horses.GetHorsesWithHistory;

public record CareerDTO(
    [property: JsonPropertyName("first")] int First,
    [property: JsonPropertyName("second")] int Second,
    [property: JsonPropertyName("third")] int Third
);

public record HashInfoDTO(
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("hex_code")] string HexCode,
    [property: JsonPropertyName("name")] string Name
);

public record HorseWithHistoryDTO(
    [property: JsonPropertyName("bloodline")] string Bloodline,
    [property: JsonPropertyName("breed_type")] string BreedType,
    [property: JsonPropertyName("career")] CareerDTO Career,
    [property: JsonPropertyName("class")] int Class,
    [property: JsonPropertyName("genotype")] string Genotype,
    [property: JsonPropertyName("hash_info")] HashInfoDTO HashInfo,
    [property: JsonPropertyName("horse_id")] int HorseId,
    [property: JsonPropertyName("horse_type")] string HorseType,
    [property: JsonPropertyName("img_url")] string ImgUrl,
    [property: JsonPropertyName("is_trial_horse")] bool IsTrialHorse,
    [property: JsonPropertyName("owner")] string Owner,
    [property: JsonPropertyName("owner_stable")] string OwnerStable,
    [property: JsonPropertyName("rating")] double Rating,
    [property: JsonPropertyName("skin")] object Skin,
    [property: JsonPropertyName("surface_preference")] int SurfacePreference,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("win_rate")] double WinRate
);

