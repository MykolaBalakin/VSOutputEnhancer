using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSColorfullOutput.Parsers {
    internal abstract class BuildMessage {
        public String Message { get; protected set; }

        public Span Span { get; protected set; }

        public BuildMessageType MessageType { get; protected set; }
    }
}
