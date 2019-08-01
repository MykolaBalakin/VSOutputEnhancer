using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildResultSucceeded)]
    [Name(ClassificationType.BuildResultSucceeded)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class BuildResultSucceededFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildResultSucceededFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}