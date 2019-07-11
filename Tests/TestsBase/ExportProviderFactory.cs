using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Balakin.VSOutputEnhancer.Tests
{
    [ExcludeFromCodeCoverage]
    public static class ExportProviderFactory
    {
        public static ExportProvider Create()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ClassificationType).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            var container = new CompositionContainer(catalog);
            return container;
        }
    }
}