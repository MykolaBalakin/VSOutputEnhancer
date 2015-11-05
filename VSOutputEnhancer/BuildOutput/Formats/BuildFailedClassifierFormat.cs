using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.BuildOutput.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildResultFailed)]
    [Name(ClassificationType.BuildResultFailed)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildResultFailedClassifierFormat : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildResultFailedClassifierFormat(StyleManager styleManager) : base(styleManager) {
            // TODO: Move to resources
            DisplayName = "Build failed";
        }
    }
}
