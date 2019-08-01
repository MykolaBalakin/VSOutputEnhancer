using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.UI.Tests
{
    [ExcludeFromCodeCoverage]
    public class TextFormattingRunPropertiesFactoryTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void CreateFillsBrushes(Color? foreground, Color? background)
        {
            var factory = new TextFormattingRunPropertiesFactory();
            var textProperties = factory.Create(foreground, background);

            textProperties.BackgroundBrushEmpty.Should().Be(!background.HasValue);
            textProperties.ForegroundBrushEmpty.Should().Be(!foreground.HasValue);
            TestSolidColorBrush(textProperties.BackgroundBrush, background);
            TestSolidColorBrush(textProperties.ForegroundBrush, foreground);
        }

        public static IEnumerable<Object[]> CreateTestData()
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