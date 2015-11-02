using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.BuildOutput.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "BuildSucceeded")]
    [Name("BuildSucceeded")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildSucceededClassifierFormat : ClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildSucceededClassifierFormat(ColorManager colorManager) {
            DisplayName = "Build succeeded";
            ForegroundColor = colorManager.SuccessColor;
        }
    }
}
