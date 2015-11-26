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
        public static ClassificationTypeDefinition BuildResultSucceeded;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.BuildResultFailed)]
        public static ClassificationTypeDefinition BuildResultFailed;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.BuildMessageError)]
        public static ClassificationTypeDefinition BuildMessageError;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.BuildMessageWarning)]
        public static ClassificationTypeDefinition BuildMessageWarning;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.PublishResultFailed)]
        public static ClassificationTypeDefinition PublishResultFailed;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.PublishResultSucceeded)]
        public static ClassificationTypeDefinition PublishResultSucceeded;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.DebugTraceWarning)]
        public static ClassificationTypeDefinition DebugTraceWarning;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.DebugTraceError)]
        public static ClassificationTypeDefinition DebugTraceError;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.DebugTraceInformation)]
        public static ClassificationTypeDefinition DebugTraceInformation;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(ClassificationType.DebugException)]
        public static ClassificationTypeDefinition DebugException;
    }
}
