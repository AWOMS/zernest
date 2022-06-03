namespace AWOMS.Zernest.Components.Extensions;

public static class ClassesExtension
{
    public static string ToClassDisplay(this int value)
    {
        if (value == Zernest.Models.ZedRun.Classes.All) return "All";
        if (value == Zernest.Models.ZedRun.Classes.Discovery) return "Discovery";
        if (value == Zernest.Models.ZedRun.Classes.Open) return "Open";
        return value.ToRoman();
    }
}