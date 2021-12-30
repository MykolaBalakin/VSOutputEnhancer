using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.DebugTraceInformation)]
    [Name(ClassificationType.DebugTraceInformation)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class DebugTraceInformationFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public DebugTraceInformationFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}