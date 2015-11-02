using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.BuildOutput.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "BuildError")]
    [Name("BuildError")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildErrorClassifierFormat : ClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildErrorClassifierFormat(ColorManager colorManager) {
            DisplayName = "Build error";
            ForegroundColor = colorManager.ErrorColor;
        }
    }
}
