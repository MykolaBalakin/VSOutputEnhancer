using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.NpmMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Classifiers.NpmMessage
{
    [Export(typeof(ISpanClassifier))]
    public class NpmMessageClassifier : ParserBasedSpanClassifier<NpmMessageData>
    {
        [ImportingConstructor]
        public NpmMessageClassifier(IParser<NpmMessageData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.Output,
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, NpmMessageData parsedData)
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