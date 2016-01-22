using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildOrderOutputClassifierTests : BuildOutputClassifierTests {
        protected override IClassifier CreateClassifier() {
            return Utils.CreateBuildOrderOutputClassifier();
        }
    }
}
