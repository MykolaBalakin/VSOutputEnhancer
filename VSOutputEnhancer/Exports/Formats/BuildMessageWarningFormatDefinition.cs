﻿using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.BuildMessageWarning)]
    [Name(ClassificationType.BuildMessageWarning)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class BuildMessageWarningFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public BuildMessageWarningFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}