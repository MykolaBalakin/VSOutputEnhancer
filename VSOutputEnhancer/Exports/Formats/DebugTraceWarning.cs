using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugTraceWarning)]
    [Name(ClassificationType.DebugTraceWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class DebugTraceWarning : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public DebugTraceWarning(IStyleManager styleManager) : base(styleManager) { }
    }
}
