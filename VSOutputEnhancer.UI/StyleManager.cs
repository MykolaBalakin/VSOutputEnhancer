using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Balakin.VSOutputEnhancer.UI
{
    [Export(typeof(IStyleManager))]
    public class StyleManager : IStyleManager
    {
        private readonly IEnvironmentService environmentService;
        private readonly Lazy<IDictionary<String, FormatDefinitionStyle>> styles;

        [ImportingConstructor]
        public StyleManager(IEnvironmentService environmentService)
        {
            this.environmentService = environmentService;
            styles = new Lazy<IDictionary<String, FormatDefinitionStyle>>(LoadColorsFromResources);
        }

        public FormatDefinitionStyle GetStyleForClassificationType(String classificationType)
        {
            if (styles.Value.TryGetValue(classificationType, out var style))
            {
                return style;
            }

            return new FormatDefinitionStyle();
        }

        private IDictionary<String, FormatDefinitionStyle> LoadColorsFromResources()
        {
            var theme = environmentService.GetTheme();
            var file = Path.Combine(GetExtensionRootPath(), "Themes", theme + "Theme.json");
            if (File.Exists(file))
            {
                var content = File.ReadAllText(file);
                try
                {
                    return JsonConvert.DeserializeObject<Dictionary<String, FormatDefinitionStyle>>(content);
                }
                catch (JsonSerializationException)
                {
                    return GetDefaultStyles();
                }
            }

            return GetDefaultStyles();
        }

        private String GetExtensionRootPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var assemblyPath = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(assemblyPath);
        }

        private IDictionary<String, FormatDefinitionStyle> GetDefaultStyles()
        {
            return new Dictionary<String, FormatDefinitionStyle>();
        }
    }
}