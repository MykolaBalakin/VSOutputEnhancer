using System;

namespace Balakin.VSOutputEnhancer.Parsers.BuildMessage {
    enum BuildMessageType {
        // Leave "0" for some sort of default or unknown value
        Warning = 1,
        Error
    }
}