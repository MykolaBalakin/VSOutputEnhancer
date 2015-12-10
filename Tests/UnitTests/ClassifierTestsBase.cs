using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    public abstract class ClassifierTestsBase {
        protected abstract IClassifier CreateClassifier();
    }
}
