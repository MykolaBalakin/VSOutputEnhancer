using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildMessageError)]
    [Name(ClassificationType.BuildMessageError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildMessageErrorFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildMessageErrorFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}