using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.VSOutputEnhancer.Parsers.NpmMessage {
    internal class NpmMessageData : ParsedData {
        public ParsedValue<String> Message { get; set; }
        public ParsedValue<MessageType> Type { get; set; }
    }
}
