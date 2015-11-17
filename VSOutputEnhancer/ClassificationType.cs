using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public const String PublishResultFailed = "PublishResultFailed";
        public const String PublishResultSucceeded = "PublishResultSucceeded";
    }
}
