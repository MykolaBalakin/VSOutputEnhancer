using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal abstract class BuildMessage {
        public String Message { get; protected set; }

        public Span Span { get; protected set; }

        public BuildMessageType MessageType { get; protected set; }
    }
}
