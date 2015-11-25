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

        public static Double GetLightness(this Color c) {
            var r = (Double)c.R / Byte.MaxValue;
            var g = (Double)c.G / Byte.MaxValue;
            var b = (Double)c.B / Byte.MaxValue;
            return (Math.Max(Math.Max(r, g), b) + Math.Min(Math.Min(r, g), b)) / 2;
        }
    }
}