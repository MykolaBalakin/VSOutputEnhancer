using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace Balakin.VSOutputEnhancer {
    internal static class Utils {
        public static String GetExtensionRootPath() {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var assemblyPath = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(assemblyPath);
        }

        public static Double GetBrightness(this Color c) {
            return Math.Sqrt(
                c.R * c.R * .241 +
                c.G * c.G * .691 +
                c.B * c.B * .068) / 255;
        }
    }
}