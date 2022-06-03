namespace AWOMS.Zernest.Models.ZedRun;

public static class Distances
{
    public const int m1000 = 1000;
    public const int m1200 = 1200;
    public const int m1400 = 1400;
    public const int m1600 = 1600;
    public const int m1800 = 1800;
    public const int m2000 = 2000;
    public const int m2200 = 2200;
    public const int m2400 = 2400;
    public const int m2600 = 2600;
}

public class Distance
{
    public int Meters { get; set; }
    public DistanceCategory Category { get; set; }
}