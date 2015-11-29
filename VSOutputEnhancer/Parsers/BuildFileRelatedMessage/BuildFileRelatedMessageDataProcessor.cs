using System;
using System.Collections.Generic;
using Balakin.VSOutputEnhancer.Parsers.BuildMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage {
    internal class BuildFileRelatedMessageDataProcessor : IParsedDataProcessor<BuildFileRelatedMessageData> {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, BuildFileRelatedMessageData parsedData) {
            if (parsedData == null) {
                yield break;
            }

            var classificationType = GetClassificationType(parsedData.Type);
            yield return new ProcessedParsedData(parsedData.FullMessage.Span, classificationType);
        }

        private String GetClassificationType(BuildMessageType messageType) {
            switch (messageType) {
                case BuildMessageType.Warning:
                    return ClassificationType.BuildMessageWarning;
                case BuildMessageType.Error:
                    return ClassificationType.BuildMessageError;
                default:
                    return null;
            }
        }
    }
}