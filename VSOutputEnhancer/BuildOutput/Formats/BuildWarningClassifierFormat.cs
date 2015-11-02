using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.BuildOutput.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "BuildWarning")]
    [Name("BuildWarning")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class BuildWarningClassifierFormat : ClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildWarningClassifierFormat(ColorManager colorManager) {
            DisplayName = "Build warning";
            ForegroundColor = colorManager.WarningColor;
        }
    }
}
