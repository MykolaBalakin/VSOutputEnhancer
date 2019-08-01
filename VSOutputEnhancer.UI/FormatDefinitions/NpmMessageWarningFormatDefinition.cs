using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.NpmMessageWarning)]
    [Name(ClassificationType.NpmMessageWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class NpmMessageWarningFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public NpmMessageWarningFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}