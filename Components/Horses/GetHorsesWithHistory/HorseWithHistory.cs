namespace AWOMS.Zernest.Components.Horses.GetHorsesWithHistory;

public class HorseWithHistory
{
	public string Bloodline { get; init; }

	public string BreedType { get; init; }

	public Career Career { get; set; }

	public int Class { get; init; }

	public string Genotype { get; init; }

	public HashInfo HashInfo { get; set; }

	public int HorseId { get; init; }

	public string HorseType { get; init; }

	public string ImgUrl { get; init; }

	public bool IsTrialHorse { get; init; }

	public string Owner { get; init; }

	public string OwnerStable { get; init; }

	public double Rating { get; init; }

	public object Skin { get; init; }

	public int SurfacePreference { get; init; }

	public string Type { get; init; }

	public double WinRate { get; init; }
}
