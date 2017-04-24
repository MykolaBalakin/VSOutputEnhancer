namespace Balakin.VSOutputEnhancer.Parsers
{
    // TODO: Review accessibility level
    public enum MessageType
    {
        // Leave "0" for some sort of default or unknown value

        [EnumValue("warning")]
        [EnumValue("warn")]
        Warning = 1,

        [EnumValue("error")]
        [EnumValue("fatal error")]
        [EnumValue("err!")]
        Error
    }
}