using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Formatting;

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

        public static Theme? GetThemeFromTextProperties(TextFormattingRunProperties properties) {
            if (!properties.BackgroundBrushEmpty) {
                var solidColorBrush = properties.BackgroundBrush as SolidColorBrush;
                if (solidColorBrush != null) {
                    if (solidColorBrush.Color.GetLightness() < 0.5) {
                        return Theme.Dark;
                    }
                    return Theme.Light;
                }
            }
            if (!properties.ForegroundBrushEmpty) {
                var solidColorBrush = properties.ForegroundBrush as SolidColorBrush;
                if (solidColorBrush != null) {
                    if (solidColorBrush.Color.GetLightness() < 0.5) {
                        return Theme.Light;
                    }
                    return Theme.Dark;
                }
            }
            return null;
        }
    }
}