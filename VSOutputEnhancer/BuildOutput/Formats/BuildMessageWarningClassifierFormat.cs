using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.BuildOutput.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildMessageWarning)]
    [Name(ClassificationType.BuildMessageWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildMessageWarningClassifierFormat : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildMessageWarningClassifierFormat(StyleManager styleManager) : base(styleManager) {
            // TODO: Move to resources
            DisplayName = "Build warning message";
        }
    }
}
