using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    public class ParsedData {
        public static T Create<T>(Match match, Span originalSpan)
            where T : ParsedData, new() {
            var type = typeof(T);
            var valueType = typeof(ParsedValue<>);
            var result = new T();

            var properties = type.GetProperties()
                .Where(p => p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == valueType);
            foreach (var property in properties) {
                var matchGroup = match.Groups[property.Name];
                if (!matchGroup.Success) {
                    var emptyValue = Activator.CreateInstance(property.PropertyType);
                    property.GetSetMethod(true).Invoke(result, new[] { emptyValue });

                    continue;
                }

                var parsedValueType = property.PropertyType.GetGenericArguments()[0];
                Object value;
                if (parsedValueType.IsEnum) {
                    value = Enum.Parse(parsedValueType, matchGroup.Value, true);
                } else {
                    value = Convert.ChangeType(matchGroup.Value, parsedValueType);
                }

                var span = new Span(originalSpan.Start + matchGroup.Index, matchGroup.Length);

                var parsedValue = Activator.CreateInstance(property.PropertyType, value, span);
                property.GetSetMethod(true).Invoke(result, new[] { parsedValue });
            }

            result.Fill(match, originalSpan);

            return result;
        }

        protected virtual void Fill(Match match, Span originalSpan) {
        }
    }
}