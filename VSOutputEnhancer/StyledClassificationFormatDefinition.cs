using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Balakin.VSOutputEnhancer.Properties;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer
{
    internal class StyledClassificationFormatDefinition : ClassificationFormatDefinition
    {
        private String GetClassificationTypeName()
        {
            var type = GetType();
            var attribute = type.GetCustomAttribute<ClassificationTypeAttribute>();
            return attribute.ClassificationTypeNames;
        }

        private readonly Lazy<String> classificationTypeName;

        protected String ClassificationTypeName
        {
            get { return classificationTypeName.Value; }
        }

        private StyledClassificationFormatDefinition()
        {
            classificationTypeName = new Lazy<String>(GetClassificationTypeName);

            var displayName = Resources.ResourceManager.GetString($"FormatDisplayName_{ClassificationTypeName}");
            DisplayName = displayName;
        }

        protected StyledClassificationFormatDefinition(IStyleManager styleManager) : this()
        {
            var style = styleManager.GetStyleForClassificationType(ClassificationTypeName);
            ForegroundColor = style.ForegroundColor;
            IsBold = style.Bold;
        }
    }
}