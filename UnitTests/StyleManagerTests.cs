using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Formatting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StyleManagerTests {
        [TestMethod]
        public void GetTheme() {
            TestTheme(null, null, null);

            TestTheme(Colors.Black, null, Theme.Light);
            TestTheme(null, Colors.White, Theme.Light);
            TestTheme(Colors.Black, Colors.White, Theme.Light);

            TestTheme(Colors.White, null, Theme.Dark);
            TestTheme(null, Colors.Black, Theme.Dark);
            TestTheme(Colors.White, Colors.Black, Theme.Dark);
        }

        private void TestTheme(Color? foreground, Color? background, Theme? expectedTheme) {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            var theme = VSOutputEnhancer.Utils.GetThemeFromTextProperties(textProperties);
            Assert.AreEqual(expectedTheme, theme);
        }
    }
}
