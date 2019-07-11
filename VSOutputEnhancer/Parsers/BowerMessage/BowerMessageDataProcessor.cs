using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BowerMessage
{
    [Obsolete]
    internal class BowerMessageDataProcessor : IParsedDataProcessor<BowerMessageData>
    {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, BowerMessageData parsedData)
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

            yield return new ProcessedParsedData(parsedData.Message.Span, classificationType);
        }

        private String GetClassificationType(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Error:
                    return ClassificationType.BowerMessageError;
                default:
                    return null;
            }
        }
    }
}