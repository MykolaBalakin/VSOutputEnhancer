using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers
{
    public class ParsedData
    {
        public static T Create<T>(Match match, Span originalSpan)
            where T : ParsedData, new()
        {
            var creator = GetParsedDataCreator(typeof(T));
            var result = (T) creator(match, originalSpan);

            result.Fill(match, originalSpan);

            return result;
        }

        private static readonly ConcurrentDictionary<Type, Func<Match, Span, ParsedData>> ParsedDataCreators = new ConcurrentDictionary<Type, Func<Match, Span, ParsedData>>();

        private static Func<Match, Span, ParsedData> GetParsedDataCreator(Type parsedDataType)
        {
            return ParsedDataCreators.GetOrAdd(parsedDataType, CreateParsedDataCreator);
        }

        private static Func<Match, Span, ParsedData> CreateParsedDataCreator(Type parsedDataType)
        {
            // This code generates method that fills ParsedData instance.
            // Generated method looks like:

            //  1| var result = new TargetParsedData();
            //  2| var matchGroups = match.Groups;
            //  3| Group matchGroup;
            //  4| Span newSpan;

            // For each ParsedData property following code will be generated:

            //  5| matchGroup = matchGroups["{{Property name}}"];
            //  6| if (matchGroup.Success) {
            //  7|     var value = ConvertType(matchGroup.Value);
            //  8|     newSpan = new Span(originalSpan.Start + matchGroup.Index, matchGroup.Length);
            //  9|     result.{{Property name}} = new ParsedValue(ConvertType(matchGroup.Value), originalSpan)
            // 10| }
            // 11| newSpan = new Span(originalSpan.Start + matchGroup.Index, matchGroup.Length);

            // 12| return result;

            var groupSuccess = typeof(Group).GetProperty("Success");
            var enumParse = typeof(Enum).GetMethod("Parse", new[] { typeof(Type), typeof(String), typeof(Boolean) });
            var groupValue = typeof(Group).GetProperty("Value");
            var groupIndex = typeof(Group).GetProperty("Index");
            var groupLength = typeof(Group).GetProperty("Length");
            var groupCollectionItem = typeof(GroupCollection).GetProperty("Item", new[] { typeof(String) });
            var convertChangeType = typeof(Convert).GetMethod("ChangeType", new[] { typeof(Object), typeof(Type) });
            var spanStart = typeof(Span).GetProperty("Start");
            var spanConstructor = typeof(Span).GetConstructor(new[] { typeof(Int32), typeof(Int32) });

            var body = new List<Expression>();
            var variables = new List<ParameterExpression>();
            var localVariables = new Dictionary<Type, ParameterExpression>();
            var matchParam = Expression.Parameter(typeof(Match));
            var originalSpanParam = Expression.Parameter(typeof(Span));

            //  1| var result = new TargetParsedData();
            var resultVar = Expression.Variable(parsedDataType);
            variables.Add(resultVar);
            body.Add(Expression.Assign(resultVar, Expression.New(parsedDataType)));

            //  2| var matchGroups = match.Groups;
            var matchGroupsVar = Expression.Variable(typeof(GroupCollection));
            body.Add(Expression.Assign(matchGroupsVar, Expression.Property(matchParam, typeof(Match).GetProperty("Groups"))));
            variables.Add(matchGroupsVar);

            //  3| Group matchGroup;
            var matchGroupVar = Expression.Variable(typeof(Group));
            variables.Add(matchGroupVar);

            //  4| Span newSpan;
            var newSpanVar = Expression.Variable(typeof(Span));
            variables.Add(newSpanVar);

            var valueType = typeof(ParsedValue<>);
            var propertiesToFill = parsedDataType.GetProperties()
                .Where(p => p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == valueType)
                .ToList();

            foreach (var property in propertiesToFill)
            {
                //  5| matchGroup = matchGroups["{{Property name}}"];
                body.Add(Expression.Assign(matchGroupVar, Expression.Property(matchGroupsVar, groupCollectionItem, Expression.Constant(property.Name))));

                //  6| if (matchGroup.Success) {
                //  7|     var value = ConvertType(matchGroup.Value);
                //  8|     newSpan = new Span(originalSpan.Start + matchGroup.Index, matchGroup.Length);
                //  9|     result.{{Property name}} = new ParsedValue(ConvertType(matchGroup.Value), originalSpan)
                // 10| }
                var ifThenBody = new List<Expression>();
                var propertyValueType = property.PropertyType.GetGenericArguments()[0];
                if (!localVariables.ContainsKey(propertyValueType))
                {
                    localVariables.Add(propertyValueType, Expression.Variable(propertyValueType));
                }
                var valueVar = localVariables[propertyValueType];
                Expression valueVarValue;
                if (propertyValueType.IsEnum)
                {
                    valueVarValue = Expression.Call(enumParse, Expression.Constant(propertyValueType), Expression.Property(matchGroupVar, groupValue), Expression.Constant(true));
                }
                else
                {
                    valueVarValue = Expression.Call(convertChangeType, Expression.Property(matchGroupVar, groupValue), Expression.Constant(propertyValueType));
                }
                ifThenBody.Add(Expression.Assign(valueVar, Expression.Convert(valueVarValue, propertyValueType)));

                // 11| newSpan = new Span(originalSpan.Start + matchGroup.Index, matchGroup.Length);
                var newSpanStart = Expression.Add(Expression.Property(originalSpanParam, spanStart), Expression.Property(matchGroupVar, groupIndex));
                var newSpanLength = Expression.Property(matchGroupVar, groupLength);
                var newSpanVarValue = Expression.New(spanConstructor, newSpanStart, newSpanLength);

                var constructor = property.PropertyType.GetConstructor(new Type[] { propertyValueType, typeof(Span) });
                ifThenBody.Add(Expression.Assign(Expression.Property(resultVar, property), Expression.New(constructor, valueVar, newSpanVarValue)));

                constructor = property.PropertyType.GetConstructor(Type.EmptyTypes);
                var ifElseBody = Expression.Assign(Expression.Property(resultVar, property), Expression.New(constructor));

                body.Add(Expression.IfThenElse(Expression.Property(matchGroupVar, groupSuccess), Expression.Block(ifThenBody), ifElseBody));
            }

            // 12| return result;
            var returnLabel = Expression.Label(typeof(ParsedData));
            body.Add(Expression.Return(returnLabel, resultVar));
            body.Add(Expression.Label(returnLabel, Expression.Constant(null, typeof(ParsedData))));

            variables.AddRange(localVariables.Values);

            var lambda = Expression.Lambda<Func<Match, Span, ParsedData>>(Expression.Block(variables, body), matchParam, originalSpanParam);
            return lambda.Compile();
        }


        protected virtual void Fill(Match match, Span originalSpan)
        {
        }
    }
}