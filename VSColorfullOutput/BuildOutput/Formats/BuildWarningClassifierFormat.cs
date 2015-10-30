using System;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSColorfullOutput.BuildOutput.Formats {
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
