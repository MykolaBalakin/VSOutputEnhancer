using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
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

                var parsedValue = CreateParsedValue(property.PropertyType, value, span);
                property.GetSetMethod(true).Invoke(result, new[] { parsedValue });
            }

            result.Fill(match, originalSpan);

            return result;
        }

        private static readonly ConcurrentDictionary<Type, Func<Object, Span, Object>> ParsedValueConstructors = new ConcurrentDictionary<Type, Func<Object, Span, Object>>();

        private static Object CreateParsedValue(Type type, Object value, Span span) {
            var constructor = ParsedValueConstructors.GetOrAdd(type, CreateParsedValueConstructor);
            return constructor(value, span);
        }

        private static Func<Object, Span, Object> CreateParsedValueConstructor(Type type) {
            var valueType = type.GetGenericArguments()[0];
            var constructor = type.GetConstructor(new[] { valueType, typeof(Span) });
            var valueParam = Expression.Parameter(typeof(Object));
            var spanParam = Expression.Parameter(typeof(Span));
            var lambda = Expression.Lambda<Func<Object, Span, Object>>(
                Expression.New(constructor, Expression.Convert(valueParam, valueType), spanParam),
                valueParam,
                spanParam);

            return lambda.Compile();
        }

        protected virtual void Fill(Match match, Span originalSpan) {
        }
    }
}