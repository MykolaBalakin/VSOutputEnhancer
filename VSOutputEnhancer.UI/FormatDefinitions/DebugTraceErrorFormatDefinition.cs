using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugTraceError)]
    [Name(ClassificationType.DebugTraceError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class DebugTraceErrorFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public DebugTraceErrorFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}