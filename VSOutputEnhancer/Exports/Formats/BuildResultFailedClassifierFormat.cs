using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildResultFailed)]
    [Name(ClassificationType.BuildResultFailed)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildResultFailedClassifierFormat : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildResultFailedClassifierFormat(IStyleManager styleManager) : base(styleManager) { }
    }
}
