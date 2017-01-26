using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestsTests
    {
        [TestMethod]
        public void CreateTextFormattingRunProperties()
        {
            TestCreateTextFormattingRunProperties(null, null);
            TestCreateTextFormattingRunProperties(Colors.White, null);
            TestCreateTextFormattingRunProperties(null, Colors.White);
            TestCreateTextFormattingRunProperties(Colors.White, Colors.Black);
            TestCreateTextFormattingRunProperties(Colors.Black, Colors.White);
        }

        private void TestCreateTextFormattingRunProperties(Color? foreground, Color? background)
        {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            Assert.AreEqual(!background.HasValue, textProperties.BackgroundBrushEmpty, $"Foreground: {foreground}, Background: {background}");
            Assert.AreEqual(!foreground.HasValue, textProperties.ForegroundBrushEmpty, $"Foreground: {foreground}, Background: {background}");
            TestSolidColorBrush(textProperties.BackgroundBrush, background);
            TestSolidColorBrush(textProperties.ForegroundBrush, foreground);
        }

        private void TestSolidColorBrush(Brush brush, Color? expectedColor)
        {
            if (expectedColor.HasValue)
            {
                Assert.IsInstanceOfType(brush, typeof(SolidColorBrush));
                var solidColorBrush = (SolidColorBrush) brush;
                Assert.AreEqual(expectedColor.Value, solidColorBrush.Color);
            }
            else
            {
                if (brush != null)
                {
                    Assert.AreEqual(Brushes.Transparent, brush);
                }
            }
        }
    }
}