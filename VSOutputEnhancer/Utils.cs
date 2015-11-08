using System;
using System.Windows.Media;

namespace Balakin.VSOutputEnhancer {
    internal static class Utils {
        public static Double GetBrightness(this Color c) {
            return Math.Sqrt(
                c.R * c.R * .241 +
                c.G * c.G * .691 +
                c.B * c.B * .068) / 255;
        }
    }
}