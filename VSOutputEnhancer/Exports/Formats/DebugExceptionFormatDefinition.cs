using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugException)]
    [Name(ClassificationType.DebugException)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class DebugExceptionFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public DebugExceptionFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}