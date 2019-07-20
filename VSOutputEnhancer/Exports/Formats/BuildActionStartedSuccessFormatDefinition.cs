using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildActionStartedSuccess)]
    [Name(ClassificationType.BuildActionStartedSuccess)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildActionStartedSuccessFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildActionStartedSuccessFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}