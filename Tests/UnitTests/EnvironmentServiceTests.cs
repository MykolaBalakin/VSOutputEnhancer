using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Classification.Fakes;
using Microsoft.VisualStudio.Text.Formatting;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EnvironmentServiceTests
    {
        [TestMethod]
        public void GetTheme()
        {
            var defaultTheme = Theme.Light;
            TestTheme((Color?) null, null, defaultTheme);

            TestTheme(Colors.Black, null, Theme.Light);
            TestTheme(null, Colors.White, Theme.Light);
            TestTheme(Colors.Black, Colors.White, Theme.Light);

            TestTheme(Colors.White, null, Theme.Dark);
            TestTheme(null, Colors.Black, Theme.Dark);
            TestTheme(Colors.White, Colors.Black, Theme.Dark);

            TestTheme(null, new RadialGradientBrush(), defaultTheme);
            TestTheme(new RadialGradientBrush(), null, defaultTheme);
            TestTheme(new RadialGradientBrush(), new RadialGradientBrush(), defaultTheme);

            TestTheme(new SolidColorBrush(Colors.Black), new RadialGradientBrush(), Theme.Light);
            TestTheme(new RadialGradientBrush(), new SolidColorBrush(Colors.White), Theme.Light);

            TestTheme(new SolidColorBrush(Colors.White), new RadialGradientBrush(), Theme.Dark);
            TestTheme(new RadialGradientBrush(), new SolidColorBrush(Colors.Black), Theme.Dark);
        }

        private void TestTheme(Brush foreground, Brush background, Theme? expectedTheme)
        {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            TestTheme(textProperties, expectedTheme);
        }

        private void TestTheme(Color? foreground, Color? background, Theme? expectedTheme)
        {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            TestTheme(textProperties, expectedTheme);
        }

        private void TestTheme(TextFormattingRunProperties textProperties, Theme? expectedTheme)
        {
            var classificationFormatMapService = new StubIClassificationFormatMapService();
            classificationFormatMapService.GetClassificationFormatMapString = category =>
            {
                return new StubIClassificationFormatMap
                {
                    DefaultTextPropertiesGet = () => textProperties
                };
            };

            var environmentService = Utils.CreateEnvironmentService(classificationFormatMapService);
            var theme = environmentService.GetTheme();
            Assert.AreEqual(expectedTheme, theme);
        }
    }
}