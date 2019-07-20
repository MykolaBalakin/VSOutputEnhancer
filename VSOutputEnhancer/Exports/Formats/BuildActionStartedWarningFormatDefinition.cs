using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildActionStartedWarning)]
    [Name(ClassificationType.BuildActionStartedWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildActionStartedWarningFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildActionStartedWarningFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}