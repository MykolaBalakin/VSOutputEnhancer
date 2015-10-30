using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSColorfullOutput.Parsers {
    internal class BuildFileRelatedMessage {
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

            result = localResult;
            return true;
        }

        public BuildMessageType MessageType { get; set; }
    }
}
