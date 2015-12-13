using System;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    public class ParsedValue<T> {
        public ParsedValue() {
            HasValue = false;
        }

        public ParsedValue(T value, Span span) {
            Value = value;
            Span = span;
            HasValue = true;
        }

        public T Value { get; }
        public Span Span { get; }
        public Boolean HasValue { get; }

        public static implicit operator T(ParsedValue<T> value) {
            if (!value.HasValue) {
                throw new InvalidOperationException("Value unassigned");
            }
            return value.Value;
        }
    }
}