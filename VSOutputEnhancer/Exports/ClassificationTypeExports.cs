using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports {
    internal static class ClassificationTypeExports {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.BuildResultSucceeded)]
        private static ClassificationTypeDefinition buildResultSucceededDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.BuildResultFailed)]
        private static ClassificationTypeDefinition buildResultFailedDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.BuildMessageError)]
        private static ClassificationTypeDefinition buildMessageErrorDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.BuildMessageWarning)]
        private static ClassificationTypeDefinition buildMessageWarningDefinition;
    }
}
