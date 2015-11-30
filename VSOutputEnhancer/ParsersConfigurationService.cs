using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == parserInterface));
            foreach (var parserType in allParsers) {
                var attributes = parserType.GetCustomAttributes<UseForClassificationAttribute>();
                foreach (var attribute in attributes) {
                    var dataProcessorType = attribute.DataProcessor;
                    if (dataProcessorType == null) {
                        throw new NotSupportedException("Auto mapping IParsedDataProcessor not supporting in this release");
                    }

                    var dataType = attribute.Data;
                    if (dataType == null) {
                        var parserDataTypes = parserType.GetInterfaces()
                            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == parserInterface)
                            .Select(i => i.GetGenericArguments()[0])
                            .ToList();
                        var processorDataTypes = dataProcessorType.GetInterfaces()
                            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == dataProcessorInterface)
                            .Select(i => i.GetGenericArguments()[0])
                            .ToList();
                        var firstEligebleType = processorDataTypes.Intersect(parserDataTypes).ToList();
                        if (firstEligebleType.Count==0) {
                            throw new InvalidOperationException("Can not find eligeble ParsedData type");
                        }
                        if (firstEligebleType.Count > 1) {
                            throw new InvalidOperationException("Find more the one eligeble ParsedData type");
                        }
                        dataType = firstEligebleType.Single();
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
