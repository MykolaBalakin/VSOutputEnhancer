using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    internal static class Utils {
        public static SnapshotSpan CreateSpan(String text) {
            var snapshot = new TextSnapshotStub(text);
            return new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
        }
    }
}
