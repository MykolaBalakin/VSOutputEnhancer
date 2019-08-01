using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildActionStartedError)]
    [Name(ClassificationType.BuildActionStartedError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildActionStartedErrorFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildActionStartedErrorFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}