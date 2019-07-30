using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ServiceProviderFixture : IDisposable
    {
        private Lazy<CompositionContainer> container = new Lazy<CompositionContainer>(CreateContainer);

        private static CompositionContainer CreateContainer()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ClassificationType).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ClassificationTypeRegistryServiceStub).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            var container = new CompositionContainer(catalog);
            return container;
        }

        public TService GetService<TService>()
        {
            return container.Value.GetExport<TService>().Value;
        }

        public void Dispose()
        {
            if (container != null && container.IsValueCreated)
            {
                container.Value.Dispose();
                container = null;
            }
        }
    }
}