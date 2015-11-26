using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Microsoft.Build.Framework.XamlTypes;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Formatting;
using Newtonsoft.Json;

namespace Balakin.VSOutputEnhancer {
    [Export(typeof(IStyleManager))]
    internal class StyleManager : IStyleManager {
        private enum Theme {
            Light,
            Dark
        }

        public StyleManager() {
            styles = new Lazy<IDictionary<String, FormatDefinitionStyle>>(GetColors);
        }

        [Import]
        private IClassificationFormatMapService classificationFormatMapService = null;

        private IDictionary<String, FormatDefinitionStyle> GetColors() {
            return LoadColorsFromResources();
        }

        private readonly Lazy<IDictionary<String, FormatDefinitionStyle>> styles;

        private IDictionary<String, FormatDefinitionStyle> Styles {
            get { return styles.Value; }
        }

        public FormatDefinitionStyle GetStyleForClassificationType(String classificationType) {
            if (Styles.ContainsKey(classificationType)) {
                return Styles[classificationType];
            }
            return new FormatDefinitionStyle();
        }

        private IDictionary<String, FormatDefinitionStyle> LoadColorsFromResources() {
            var theme = GetCurrentTheme();
            var file = Path.Combine(Utils.GetExtensionRootPath(), "Resources", theme + "Theme.json");
            if (File.Exists(file)) {
                var content = File.ReadAllText(file);
                try {
                    return JsonConvert.DeserializeObject<Dictionary<String, FormatDefinitionStyle>>(content);
                } catch (JsonSerializationException) {
                }
            }
            return new Dictionary<String, FormatDefinitionStyle>();
        }


        private Theme GetCurrentTheme() {
            // I don't want to reference many of COM libraries to get current VS theme
            // so I'm trying to get recognize it from current format settings
            var formatMap = classificationFormatMapService.GetClassificationFormatMap("output");
            var theme = GetThemeFromTextProperties(formatMap.DefaultTextProperties);
            if (theme != null) {
                return theme.Value;
            }

            // This code can be userd for 
            // var literalClassificationType = formatMap.CurrentPriorityOrder.FirstOrDefault(c => c != null && c.Classification.Equals("literal", StringComparison.OrdinalIgnoreCase));
            // if (literalClassificationType != null) {
            //     theme = GetThemeFromTextProperties(formatMap.GetTextProperties(literalClassificationType));
            //     if (theme != null) {
            //         return theme.Value;
            //     }
            // }

            return Theme.Light;
        }

        private Theme? GetThemeFromTextProperties(TextFormattingRunProperties properties) {
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