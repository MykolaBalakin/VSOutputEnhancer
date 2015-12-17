using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Formatting;

namespace Balakin.VSOutputEnhancer {
    [Export(typeof(IEnvironmentService))]
    internal class EnvironmentService: IEnvironmentService {
        private readonly IClassificationFormatMapService classificationFormatMapService;

        [ImportingConstructor]
        public EnvironmentService(IClassificationFormatMapService classificationFormatMapService) {
            this.classificationFormatMapService = classificationFormatMapService;
        }

        public Theme GetTheme() {
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
