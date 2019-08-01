using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugException)]
    [Name(ClassificationType.DebugException)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class DebugExceptionFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public DebugExceptionFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}