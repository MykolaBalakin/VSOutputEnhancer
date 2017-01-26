using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmMessage
{
    internal class NpmMessageDataProcessor : IParsedDataProcessor<NpmMessageData>
    {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, NpmMessageData parsedData)
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
                case MessageType.Warning:
                    return ClassificationType.NpmMessageWarning;
                case MessageType.Error:
                    return ClassificationType.NpmMessageError;
                default:
                    return null;
            }
        }
    }
}