using System;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmMessage {
    [UseForClassification(ContentType.BuildOutput)]
    internal class NpmMessageParser : IParser<NpmMessageData> {
        public Boolean TryParse(SnapshotSpan span, out NpmMessageData result) {
            throw new NotImplementedException();
        }
    }
}