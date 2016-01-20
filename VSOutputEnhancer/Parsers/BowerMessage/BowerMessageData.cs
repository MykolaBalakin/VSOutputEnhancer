using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.VSOutputEnhancer.Parsers.BowerMessage {
    internal class BowerMessageData : ParsedData {
        public ParsedValue<String> PackageName { get; set; }
        public ParsedValue<String> PackageVersion { get; set; }
        public ParsedValue<MessageType> Type { get; set; }
        public ParsedValue<String> TypeCode { get; set; }
        public ParsedValue<String> Message { get; set; }
    }
}
