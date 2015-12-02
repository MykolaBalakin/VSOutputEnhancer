using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StyleManagerTests {
        [TestMethod]
        public void GetTheme() {
            TestTheme((Color?)null, null, null);

            TestTheme(Colors.Black, null, Theme.Light);
            TestTheme(null, Colors.White, Theme.Light);
            TestTheme(Colors.Black, Colors.White, Theme.Light);

            TestTheme(Colors.White, null, Theme.Dark);
            TestTheme(null, Colors.Black, Theme.Dark);
            TestTheme(Colors.White, Colors.Black, Theme.Dark);

            TestTheme(null, new RadialGradientBrush(), null);
            TestTheme(new RadialGradientBrush(), null, null);
            TestTheme(new RadialGradientBrush(), new RadialGradientBrush(), null);

            TestTheme(new SolidColorBrush(Colors.Black), new RadialGradientBrush(), Theme.Light);
            TestTheme(new RadialGradientBrush(), new SolidColorBrush(Colors.White), Theme.Light);

            TestTheme(new SolidColorBrush(Colors.White), new RadialGradientBrush(), Theme.Dark);
            TestTheme(new RadialGradientBrush(), new SolidColorBrush(Colors.Black),  Theme.Dark);
        }

        private void TestTheme(Brush foreground, Brush background, Theme? expectedTheme) {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            var theme = VSOutputEnhancer.Utils.GetThemeFromTextProperties(textProperties);
            Assert.AreEqual(expectedTheme, theme);
        }

        private void TestTheme(Color? foreground, Color? background, Theme? expectedTheme) {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            var theme = VSOutputEnhancer.Utils.GetThemeFromTextProperties(textProperties);
            Assert.AreEqual(expectedTheme, theme);
        }
    }
}
