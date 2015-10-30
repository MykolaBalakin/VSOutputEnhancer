using System;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSColorfullOutput.BuildOutput.Formats {
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
