using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildMessageWarning)]
    [Name(ClassificationType.BuildMessageWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildMessageWarningClassifierFormat : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildMessageWarningClassifierFormat(IStyleManager styleManager) : base(styleManager) { }
    }
}
