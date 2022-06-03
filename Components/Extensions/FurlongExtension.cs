using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWOMS.Zernest.Components.Extensions;

public static class FurlongExtension
{
    // furlong = 1/8 mile or 660 feet or 220 yards or 201.168 meters
    public static string ToFurlong(this int meters)
    {
        // 1000 = 5f
        // 1200 = 6f
        // 1400 = 7f
        // 1600 = 8f
        // 1800 = 9f
        // 2000 = 10f
        // 2200 = 11f
        // 2400 = 12f
        // 2600 = 13f
        var furlongs = meters / 201.168;
        return Math.Round(furlongs).ToString();
    }
}