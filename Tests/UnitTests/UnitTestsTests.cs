using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class UnitTestsTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void CreateTextFormattingRunProperties(Color? foreground, Color? background)
        {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            textProperties.BackgroundBrushEmpty.Should().Be(!background.HasValue, $"Foreground: {foreground}, Background: {background}");
            textProperties.ForegroundBrushEmpty.Should().Be(!foreground.HasValue, $"Foreground: {foreground}, Background: {background}");
            TestSolidColorBrush(textProperties.BackgroundBrush, background);
            TestSolidColorBrush(textProperties.ForegroundBrush, foreground);
        }

        public static IEnumerable<object[]> CreateTestData()
        {
            yield return new Object[] { null, null };
            yield return new Object[] { Colors.White, null };
            yield return new Object[] { null, Colors.White };
            yield return new Object[] { Colors.White, Colors.Black };
            yield return new Object[] { Colors.Black, Colors.White };
        }

        private void TestSolidColorBrush(Brush brush, Color? expectedColor)
        {
            if (expectedColor.HasValue)
            {
                brush.Should().BeOfType<SolidColorBrush>();
                var solidColorBrush = (SolidColorBrush) brush;
                solidColorBrush.Color.Should().Be(expectedColor.Value);
            }
            else
            {
                if (brush != null)
                {
                    brush.Should().Be(Brushes.Transparent);
                }
            }
        }
    }
}