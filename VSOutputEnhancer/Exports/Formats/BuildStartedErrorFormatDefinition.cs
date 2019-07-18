using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildStartedError)]
    [Name(ClassificationType.BuildStartedError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildStartedErrorFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildStartedErrorFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}