using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BowerMessageError)]
    [Name(ClassificationType.BowerMessageError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class BowerMessageErrorFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BowerMessageErrorFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}