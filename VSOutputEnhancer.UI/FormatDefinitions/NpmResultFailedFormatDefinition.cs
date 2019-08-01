using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.NpmResultFailed)]
    [Name(ClassificationType.NpmResultFailed)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class NpmResultFailedFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public NpmResultFailedFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}