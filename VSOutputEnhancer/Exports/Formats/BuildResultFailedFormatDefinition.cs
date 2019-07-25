using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildResultFailed)]
    [Name(ClassificationType.BuildResultFailed)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildResultFailedFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildResultFailedFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}