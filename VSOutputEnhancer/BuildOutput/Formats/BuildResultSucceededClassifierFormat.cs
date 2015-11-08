using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.BuildOutput.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildResultSucceeded)]
    [Name(ClassificationType.BuildResultSucceeded)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildResultSucceededClassifierFormat : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildResultSucceededClassifierFormat(StyleManager styleManager) : base(styleManager) {
            // TODO: Move to resources
            DisplayName = "Build succeeded";
        }
    }
}
