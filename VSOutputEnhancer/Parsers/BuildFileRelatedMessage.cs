using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    [Obsolete]
    internal class BuildFileRelatedMessage : BuildMessage {
        public static Boolean TryParse(SnapshotSpan span, out BuildFileRelatedMessage result) {
            result = null;
            var text = span.GetText();
            var regex = "^\\d+>(?<Path>.*): (?<Type>warning|error) (?<Code>\\w+): (?<Message>.*)\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            var localResult = new BuildFileRelatedMessage();
            var messageType = match.Groups["Type"].Value;
            if (messageType.Equals("warning", StringComparison.Ordinal)) {
                localResult.MessageType = BuildMessageType.Warning;
            } else if (messageType.Equals("error", StringComparison.Ordinal)) {
                localResult.MessageType = BuildMessageType.Error;
            }

            localResult.Message = match.Groups["Message"].Value;
            localResult.Span = new Span(match.Groups["Type"].Index, match.Groups["Message"].Index + match.Groups["Message"].Length - match.Groups["Type"].Index);

            result = localResult;
            return true;
        }

        public String FilePath { get; private set; }

        public String RelatedFilePath { get; private set; }
    }
}
