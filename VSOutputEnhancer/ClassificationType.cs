using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.BuildOutput.Formats;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer {
    internal static class ClassificationType {
        public static readonly String[] All = new[] {
            BuildMessageError,
            BuildMessageWarning,
            BuildResultFailed,
            BuildResultSucceeded
        };

        public const String BuildMessageError = "BuildMessageError";
        public const String BuildMessageWarning = "BuildMessageWarning";

        public const String BuildResultFailed = "BuildResultFailed";
        public const String BuildResultSucceeded = "BuildResultSucceeded";

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
