using System;

namespace AWOMS.Zernest.Components.Horses.GetStableHorses;

public class StableHorse
{
	public string Bloodline { get; init; }

	public string BreedType { get; init; }

	public int BreedingCounter { get; init; }

	public DateTime BreedingCycleReset { get; init; }

	public int BreedingDecayLevel { get; init; }

	public int BreedingDecayLimit { get; init; }

	public Career Career { get; set; }

	public HashInfo HashInfo { get; set; }

	public int Class { get; init; }

	public object CurrentFatigue { get; init; }

	public string Gender { get; init; }

	public string Genotype { get; init; }

	public int HorseId { get; init; }

	public string ImgUrl { get; init; }

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

	public string Owner { get; init; }

	public double Rating { get; init; }

	public object Skin { get; init; }

	public bool SuperCoat { get; init; }

	public int SurfacePreference { get; init; }

	public string Tx { get; init; }

	public DateTime TxDate { get; init; }

	public double WinRate { get; init; }
}
