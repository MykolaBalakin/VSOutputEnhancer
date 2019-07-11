using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage
{
    [Obsolete]
    internal class BuildFileRelatedMessageDataProcessor : IParsedDataProcessor<BuildFileRelatedMessageData>
    {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, BuildFileRelatedMessageData parsedData)
        {
            if (parsedData == null)
            {
                yield break;
            }

            var classificationType = GetClassificationType(parsedData.Type);
            if (String.IsNullOrEmpty(classificationType))
            {
                yield break;
            }
            yield return new ProcessedParsedData(parsedData.FullMessage.Span, classificationType);
        }

        private String GetClassificationType(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Warning:
                    return ClassificationType.BuildMessageWarning;
                case MessageType.Error:
                    return ClassificationType.BuildMessageError;
                default:
                    return null;
            }
        }
    }
}