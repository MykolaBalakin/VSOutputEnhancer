using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Balakin.VSOutputEnhancer.Tests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class EnvironmentServiceStub : IEnvironmentService
    {
        private readonly Theme theme;

        public EnvironmentServiceStub(Theme theme)
        {
            this.theme = theme;
        }

        public Theme GetTheme()
        {
            return theme;
        }
    }
}