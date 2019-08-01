using System;
using System.Windows.Media;

namespace Balakin.VSOutputEnhancer.UI
{
    public static class ColorExtensions
    {
        public static Double GetLightness(this Color c)
        {
            var r = (Double) c.R / Byte.MaxValue;
            var g = (Double) c.G / Byte.MaxValue;
            var b = (Double) c.B / Byte.MaxValue;
            return (Math.Max(Math.Max(r, g), b) + Math.Min(Math.Min(r, g), b)) / 2;
        }
    }
}