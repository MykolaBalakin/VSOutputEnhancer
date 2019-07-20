using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BowerMessage
{
    [Export(typeof(ISpanClassifier))]
    public class BowerMessageClassifier : ParserBasedSpanClassifier<BowerMessageData>
    {
        [ImportingConstructor]
        public BowerMessageClassifier(IParser<BowerMessageData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.Output
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, BowerMessageData parsedData, DataContainer data)
        {
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