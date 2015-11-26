using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugTraceError)]
    [Name(ClassificationType.DebugTraceError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class DebugTraceError : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public DebugTraceError(IStyleManager styleManager) : base(styleManager) { }
    }
}
