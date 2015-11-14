using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildMessageError)]
    [Name(ClassificationType.BuildMessageError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildMessageErrorClassifierFormat : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public BuildMessageErrorClassifierFormat(IStyleManager styleManager) : base(styleManager) { }
    }
}
