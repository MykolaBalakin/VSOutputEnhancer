using System;

namespace Balakin.VSOutputEnhancer
{
    public class ParserConfiguration
    {
        public ParserConfiguration(Type parser, Type data, Type dataProcessor)
        {
            Parser = parser;
            Data = data;
            DataProcessor = dataProcessor;
        }

        public Type Parser { get; }
        public Type Data { get; }
        public Type DataProcessor { get; }
    }
}