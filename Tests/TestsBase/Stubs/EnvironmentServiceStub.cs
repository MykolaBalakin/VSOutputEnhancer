using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.VSOutputEnhancer.Tests.Stubs {
    [ExcludeFromCodeCoverage]
    public class EnvironmentServiceStub:IEnvironmentService {
        private readonly Theme theme;

        public EnvironmentServiceStub(Theme theme) {
            this.theme = theme;
        }

        public Theme GetTheme() {
            return theme;
        }
    }
}
