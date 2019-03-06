using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildProjectHeader)]
    [Name(ClassificationType.BuildProjectHeader)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildProjectHeaderFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildProjectHeaderFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}