using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Threading;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer {
    [Export(typeof(IParsersConfigurationService))]
    internal class ParsersConfigurationService : IParsersConfigurationService {
        private class InternalParserConfigurationEntry {
            public InternalParserConfigurationEntry(String contentType, ParserConfiguration configuration) {
                ContentType = contentType;
                Configuration = configuration;
            }

            public String ContentType { get; }
            public ParserConfiguration Configuration { get; }
        }

        static ParsersConfigurationService() {
            configuration = new Lazy<IList<InternalParserConfigurationEntry>>(LoadConfiguration, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private static readonly Lazy<IList<InternalParserConfigurationEntry>> configuration;
        private static IList<InternalParserConfigurationEntry> LoadConfiguration() {
            var result = new List<InternalParserConfigurationEntry>();

            var parserInterface = typeof(IParser<>);
            var dataProcessorInterface = typeof(IParsedDataProcessor<>);

            var assembly = Assembly.GetExecutingAssembly();
            var allParsers = assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Select(t => new {
                    Parser = t,
                    DataTypes = t.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == parserInterface)
                        .Select(i => i.GetGenericArguments()[0]).ToList()
                })
                .Where(t => t.DataTypes.Any())
                .ToDictionary(e => e.Parser, e => e.DataTypes);
            var allDataProcessors = assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Select(t => new {
                    DataProcessor = t,
                    DataTypes = t.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == dataProcessorInterface)
                        .Select(i => i.GetGenericArguments()[0]).ToList()
                })
                .Where(t => t.DataTypes.Any())
                .ToDictionary(e => e.DataProcessor, e => e.DataTypes);
            foreach (var parserDefinition in allParsers) {
                var parserType = parserDefinition.Key;
                var attributes = parserType.GetCustomAttributes<UseForClassificationAttribute>();
                foreach (var attribute in attributes) {
                    var dataProcessorType = attribute.DataProcessor;
                    var dataType = attribute.Data;

                    if (dataProcessorType == null && dataType == null) {
                        var dataProcessorDefinitions = allDataProcessors
                            .Where(dp => dp.Value.Any(dpt => parserDefinition.Value.Contains(dpt)))
                            .ToList();
                        if (dataProcessorDefinitions.Count == 0) {
                            throw new InvalidOperationException($"Can not find eligible IParsedDataProcessor for parser {parserType.Name}");
                        }
                        if (dataProcessorDefinitions.Count > 1) {
                            throw new InvalidOperationException($"Find more the one eligible IParsedDataProcessor for parser {parserType.Name}");
                        }
                        dataProcessorType = dataProcessorDefinitions.Single().Key;
                    } else if (dataProcessorType == null) {
                        var dataProcessorDefinitions = allDataProcessors
                            .Where(dp => dp.Value.Contains(dataType))
                            .ToList();
                        if (dataProcessorDefinitions.Count == 0) {
                            throw new InvalidOperationException($"Can not find eligible IParsedDataProcessor for ParsedData type {dataType.Name}");
                        }
                        if (dataProcessorDefinitions.Count > 1) {
                            throw new InvalidOperationException($"Find more the one eligible IParsedDataProcessor for ParsedData type {dataType.Name}");
                        }
                        dataProcessorType = dataProcessorDefinitions.Single().Key;
                    }

                    if (dataType == null) {
                        var eligebleTypes = allDataProcessors[dataProcessorType].Intersect(allParsers[parserType]).ToList();
                        if (eligebleTypes.Count == 0) {
                            throw new InvalidOperationException($"Can not find eligible ParsedData type for parser {parserType.Name} and processor {dataProcessorType.Namespace}");
                        }
                        if (eligebleTypes.Count > 1) {
                            throw new InvalidOperationException($"Find more the one eligible ParsedData type for parser {parserType.Name} and processor {dataProcessorType.Namespace}");
                        }
                        dataType = eligebleTypes.Single();
                    }

                    var configurationEntry = new ParserConfiguration(parserType, dataType, dataProcessorType);
                    result.Add(new InternalParserConfigurationEntry(attribute.ContentType, configurationEntry));
                }
            }
            return result.AsReadOnly();
        }
        private static IList<InternalParserConfigurationEntry> Configuration => configuration.Value;

        public IEnumerable<ParserConfiguration> GetParsers(IContentType contentType) {
            foreach (var entry in Configuration) {
                if (entry.ContentType.Equals(contentType.TypeName, StringComparison.OrdinalIgnoreCase)) {
                    yield return entry.Configuration;
                }
            }
        }
    }
}
