using System.Text.Json.Serialization;

namespace AWOMS.Zernest.Components.ZedRun.Features.Horses.GetStableHorses;

public record StableHorseDTO(
    [property: JsonPropertyName("bloodline")] string Bloodline,
    [property: JsonPropertyName("breed_type")] string BreedType,
    [property: JsonPropertyName("breeding_counter")] int BreedingCounter,
    [property: JsonPropertyName("breeding_cycle_reset")] DateTime BreedingCycleReset,
    [property: JsonPropertyName("breeding_decay_level")] int BreedingDecayLevel,
    [property: JsonPropertyName("breeding_decay_limit")] int BreedingDecayLimit,
    [property: JsonPropertyName("career")] CareerDTO Career,
    [property: JsonPropertyName("class")] int Class,
    [property: JsonPropertyName("current_fatigue")] object CurrentFatigue,    
    [property: JsonPropertyName("horse_type")] string Gender,
    [property: JsonPropertyName("genotype")] string Genotype,
    [property: JsonPropertyName("hash_info")] HashInfoDTO HashInfo,
    [property: JsonPropertyName("horse_id")] int HorseId,
    [property: JsonPropertyName("img_url")] string ImgUrl,
    [property: JsonPropertyName("ineligible_reason")] object IneligibleReason,
    [property: JsonPropertyName("is_approved_for_racing")] bool IsApprovedForRacing,
    [property: JsonPropertyName("is_available")] bool IsAvailable,
    [property: JsonPropertyName("is_being_transferred")] bool IsBeingTransferred,
    [property: JsonPropertyName("is_eligible")] object IsEligible,
    [property: JsonPropertyName("is_in_stud")] bool IsInStud,
    [property: JsonPropertyName("is_on_racing_contract")] bool IsOnRacingContract,
    [property: JsonPropertyName("is_running_free_race")] bool IsRunningFreeRace,
    [property: JsonPropertyName("is_upgraded")] bool IsUpgraded,
    [property: JsonPropertyName("last_stud_duration")] int LastStudDuration,
    [property: JsonPropertyName("last_stud_timestamp")] int LastStudTimestamp,
    [property: JsonPropertyName("mating_price")] string MatingPrice,
    [property: JsonPropertyName("number_of_races")] int NumberOfRaces,
    [property: JsonPropertyName("offspring_count")] int OffspringCount,
    [property: JsonPropertyName("offspring_count_after_decay")] int OffspringCountAfterDecay,
    [property: JsonPropertyName("owner")] string Owner,
    [property: JsonPropertyName("rating")] double Rating,
    [property: JsonPropertyName("skin")] object Skin,
    [property: JsonPropertyName("super_coat")] bool SuperCoat,
    [property: JsonPropertyName("surface_preference")] int SurfacePreference,
    [property: JsonPropertyName("tx")] string Tx,
    [property: JsonPropertyName("tx_date")] DateTime TxDate,
    [property: JsonPropertyName("win_rate")] double WinRate
);

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
