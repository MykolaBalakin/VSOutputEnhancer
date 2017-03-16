using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Threading;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer
{
    [Export(typeof(IParsersConfigurationService))]
    internal class ParsersConfigurationService : IParsersConfigurationService
    {
        private class InternalParserConfigurationEntry
        {
            public InternalParserConfigurationEntry(String contentType, ParserConfiguration configuration)
            {
                ContentType = contentType;
                Configuration = configuration;
            }

            public String ContentType { get; }
            public ParserConfiguration Configuration { get; }
        }

        static ParsersConfigurationService()
        {
            configuration = new Lazy<IReadOnlyCollection<InternalParserConfigurationEntry>>(LoadConfiguration, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private static readonly Lazy<IReadOnlyCollection<InternalParserConfigurationEntry>> configuration;

        private static IReadOnlyCollection<InternalParserConfigurationEntry> LoadConfiguration()
        {
            var result = new List<InternalParserConfigurationEntry>();

            var allParsers = GetAllParsers();
            var allDataProcessors = GetAllDataProcessors();

            foreach (var parserDefinition in allParsers)
            {
                var parserType = parserDefinition.Key;
                var parserDataTypes = parserDefinition.Value;

                var attributes = parserType.GetCustomAttributes<UseForClassificationAttribute>();
                foreach (var attribute in attributes)
                {
                    var dataProcessorType = attribute.DataProcessor;
                    var dataType = attribute.Data;

                    if (dataProcessorType == null && dataType == null)
                    {
                        var dataProcessorDefinitions = allDataProcessors
                            .Where(dp => dp.Value.Any(dpt => parserDataTypes.Contains(dpt)))
                            .ToList();
                        if (dataProcessorDefinitions.Count == 0)
                        {
                            throw new InvalidOperationException($"Can not find eligible IParsedDataProcessor for parser {parserType.Name}");
                        }
                        if (dataProcessorDefinitions.Count > 1)
                        {
                            throw new InvalidOperationException($"Have found more the one eligible IParsedDataProcessor for parser {parserType.Name}");
                        }
                        dataProcessorType = dataProcessorDefinitions.Single().Key;
                    }
                    else if (dataProcessorType == null)
                    {
                        var dataProcessorDefinitions = allDataProcessors
                            .Where(dp => dp.Value.Contains(dataType))
                            .ToList();
                        if (dataProcessorDefinitions.Count == 0)
                        {
                            throw new InvalidOperationException($"Can not find eligible IParsedDataProcessor for ParsedData type {dataType.Name}");
                        }
                        if (dataProcessorDefinitions.Count > 1)
                        {
                            throw new InvalidOperationException($"Have found more the one eligible IParsedDataProcessor for ParsedData type {dataType.Name}");
                        }
                        dataProcessorType = dataProcessorDefinitions.Single().Key;
                    }

                    if (dataType == null)
                    {
                        var eligebleTypes = allDataProcessors[dataProcessorType].Intersect(allParsers[parserType]).ToList();
                        if (eligebleTypes.Count == 0)
                        {
                            throw new InvalidOperationException($"Can not find eligible ParsedData type for parser {parserType.Name} and processor {dataProcessorType.Namespace}");
                        }
                        if (eligebleTypes.Count > 1)
                        {
                            throw new InvalidOperationException($"Have found more the one eligible ParsedData type for parser {parserType.Name} and processor {dataProcessorType.Namespace}");
                        }
                        dataType = eligebleTypes.Single();
                    }

                    var configurationEntry = new ParserConfiguration(parserType, dataType, dataProcessorType);
                    result.Add(new InternalParserConfigurationEntry(attribute.ContentType, configurationEntry));
                }
            }
            return result.AsReadOnly();
        }

        private static IReadOnlyDictionary<Type, IReadOnlyCollection<Type>> GetAllParsers()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var parserInterface = typeof(IParser<>);

            var result = assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Select(t => new
                {
                    Parser = t,
                    DataTypes = t.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == parserInterface)
                        .Select(i => i.GetGenericArguments()[0])
                        .ToList()
                        .AsReadOnly()
                })
                .Where(t => t.DataTypes.Any())
                .ToDictionary(e => e.Parser, e => e.DataTypes as IReadOnlyCollection<Type>);

            return new ReadOnlyDictionary<Type, IReadOnlyCollection<Type>>(result);
        }

        private static IReadOnlyDictionary<Type, IReadOnlyCollection<Type>> GetAllDataProcessors()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var dataProcessorInterface = typeof(IParsedDataProcessor<>);

            var result = assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Select(t => new
                {
                    DataProcessor = t,
                    DataTypes = t.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == dataProcessorInterface)
                        .Select(i => i.GetGenericArguments()[0])
                        .ToList()
                        .AsReadOnly()
                })
                .Where(t => t.DataTypes.Any())
                .ToDictionary(e => e.DataProcessor, e => e.DataTypes as IReadOnlyCollection<Type>);

            return new ReadOnlyDictionary<Type, IReadOnlyCollection<Type>>(result);
        }

        private static IReadOnlyCollection<InternalParserConfigurationEntry> Configuration => configuration.Value;

        public IReadOnlyCollection<ParserConfiguration> GetParsers(IContentType contentType)
        {
            return Configuration
                .Where(c => c.ContentType.Equals(contentType.TypeName, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Configuration)
                .ToList()
                .AsReadOnly();
        }
    }
}