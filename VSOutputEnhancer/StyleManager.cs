using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Text.Classification;
using Newtonsoft.Json;

namespace Balakin.VSOutputEnhancer {
    [Export(typeof(IStyleManager))]
    internal class StyleManager : IStyleManager {
        private readonly IEnvironmentService environmentService;

        [ImportingConstructor]
        public StyleManager(IEnvironmentService environmentService) {
            this.environmentService = environmentService;
            styles = new Lazy<IDictionary<String, FormatDefinitionStyle>>(GetColors);
        }

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
            var theme = environmentService.GetTheme();
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
    }
}