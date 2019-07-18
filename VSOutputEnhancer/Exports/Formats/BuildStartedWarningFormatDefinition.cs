using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildStartedWarning)]
    [Name(ClassificationType.BuildStartedWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildStartedWarningFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildStartedWarningFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}