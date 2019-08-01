using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildMessageError)]
    [Name(ClassificationType.BuildMessageError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class BuildMessageErrorFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildMessageErrorFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}