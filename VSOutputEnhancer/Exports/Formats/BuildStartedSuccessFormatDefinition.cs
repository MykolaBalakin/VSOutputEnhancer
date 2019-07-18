using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildStartedSuccess)]
    [Name(ClassificationType.BuildStartedSuccess)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildStartedSuccessFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildStartedSuccessFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}