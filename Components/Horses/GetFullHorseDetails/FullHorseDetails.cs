using System;

namespace AWOMS.Zernest.Components.Horses.GetFullHorseDetails;

public class FullHorseDetails
{
    // Below are common between both
    public string Bloodline { get; init; }

    public string BreedType { get; init; }

    public Career Career { get; set; }

    public int Class { get; init; }

    public string Genotype { get; init; }

    public HashInfo HashInfo { get; set; }

    public int HorseId { get; init; }

    public string ImgUrl { get; init; }

    public string Owner { get; init; }

    public double Rating { get; init; }

    public object Skin { get; init; }

    public int SurfacePreference { get; init; }

    public double WinRate { get; init; }

    // Below are from HorseWithHistory
    public bool IsTrialHorse { get; init; }

    public string OwnerStable { get; init; }

    public string Type { get; init; }

    // Below are from StableHorse
    public int BreedingCounter { get; init; }

    public DateTime BreedingCycleReset { get; init; }

    public int BreedingDecayLevel { get; init; }

    public int BreedingDecayLimit { get; init; }

    public object CurrentFatigue { get; init; }

    public string Gender { get; init; }

    public object IneligibleReason { get; init; }

    public bool IsApprovedForRacing { get; init; }

    public bool IsAvailable { get; init; }

    public bool IsBeingTransferred { get; init; }

    public object IsEligible { get; init; }

    public bool IsInStud { get; init; }

    public bool IsOnRacingContract { get; init; }

    public bool IsRunningFreeRace { get; init; }

    public bool IsUpgraded { get; init; }

    public int LastStudDuration { get; init; }

    public int LastStudTimestamp { get; init; }

    public string MatingPrice { get; init; }

    public int NumberOfRaces { get; init; }

    public int OffspringCount { get; init; }

    public int OffspringCountAfterDecay { get; init; }

    public bool SuperCoat { get; init; }

    public string Tx { get; init; }

    public DateTime TxDate { get; init; }

    // Model methods

    public int NumberOfPlaces
    {
        get
        {
            return NumberOfRaces == 0 ? 0
                : Career.First + Career.Second + Career.Third;
        }
    }

    public int NumberOfNonPlaces
    {
        get
        {
            return NumberOfRaces == 0 ? 0
                : NumberOfRaces
                        - (Career.First + Career.Second + Career.Third);
        }
    }

    public double FirstWinPct
    {
        get
        {
            return Career.First == 0
                    || NumberOfRaces == 0 ? 0
                    : Math.Round(((double)Career.First / (double)NumberOfRaces) * 100, 2);
        }
    }

    public double SecondWinPct
    {
        get
        {

            return Career.Second == 0
                    || NumberOfRaces == 0 ? 0
                    : Math.Round(((double)Career.Second / (double)NumberOfRaces) * 100, 2);
        }
    }

    public double ThirdWinPct
    {
        get
        {

            return Career.Third == 0
                    || NumberOfRaces == 0 ? 0
                    : Math.Round(((double)Career.Third / (double)NumberOfRaces) * 100, 2);
        }
    }

    public double PlacePct
    {
        get
        {

            return NumberOfRaces == 0 ? 0
                    : Math.Round((
                        (double)NumberOfPlaces / (double)NumberOfRaces
                    ) * 100, 2);
        }
    }

    public double NonPlacePct
    {
        get
        {

            return NumberOfRaces == 0 ? 0
                    : Math.Round((
                        (double)NumberOfNonPlaces / (double)NumberOfRaces
                    ) * 100, 2);
        }
    }
}
