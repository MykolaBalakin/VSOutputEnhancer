using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugTraceWarning)]
    [Name(ClassificationType.DebugTraceWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class DebugTraceWarningFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public DebugTraceWarningFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}