﻿using System;

namespace Balakin.VSOutputEnhancer.Logic
{
    public static class ClassificationType
    {
        public static readonly String[] All = new[]
        {
            BuildMessageError,
            BuildMessageWarning,
            BuildResultFailed,
            BuildResultSucceeded,
            BuildActionStartedSuccess,
            BuildActionStartedWarning,
            BuildActionStartedError,
            PublishResultFailed,
            PublishResultSucceeded,
            DebugTraceError,
            DebugTraceWarning,
            DebugTraceInformation,
            DebugException,
            NpmResultFailed,
            NpmResultSucceeded,
            NpmMessageWarning,
            NpmMessageError,
            BowerMessageError
        };

        public const String BuildMessageError = "BuildMessageError";
        public const String BuildMessageWarning = "BuildMessageWarning";

        public const String BuildResultFailed = "BuildResultFailed";
        public const String BuildResultSucceeded = "BuildResultSucceeded";

        public const String BuildActionStartedSuccess = "BuildActionStartedSuccess";
        public const String BuildActionStartedWarning = "BuildActionStartedWarning";
        public const String BuildActionStartedError = "BuildActionStartedError";

        public const String PublishResultFailed = "PublishResultFailed";
        public const String PublishResultSucceeded = "PublishResultSucceeded";

        public const String DebugTraceError = "DebugTraceError";
        public const String DebugTraceWarning = "DebugTraceWarning";
        public const String DebugTraceInformation = "DebugTraceInformation";
        public const String DebugException = "DebugException";

        public const String NpmResultFailed = "NpmResultFailed";
        public const String NpmResultSucceeded = "NpmResultSucceeded";

        public const String NpmMessageWarning = "NpmMessageWarning";
        public const String NpmMessageError = "NpmMessageError";

        public const String BowerMessageError = "BowerMessageError";
    }
}