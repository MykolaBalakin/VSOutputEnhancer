using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.NpmResultSucceeded)]
    [Name(ClassificationType.NpmResultSucceeded)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class NpmResultSucceededFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public NpmResultSucceededFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}