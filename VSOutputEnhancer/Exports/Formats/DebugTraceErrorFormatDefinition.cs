using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugTraceError)]
    [Name(ClassificationType.DebugTraceError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class DebugTraceErrorFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public DebugTraceErrorFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}