using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Formatting;
using NSubstitute;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class EnvironmentServiceTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void GetTheme(Brush foreground, Brush background, Theme? expectedTheme)
        {
            var textProperties = Utils.CreateTextFormattingRunProperties(foreground, background);
            var classificationFormatMap = Substitute.For<IClassificationFormatMap>();
            classificationFormatMap.DefaultTextProperties.Returns(textProperties);

            var classificationFormatMapService = Substitute.For<IClassificationFormatMapService>();
            classificationFormatMapService.GetClassificationFormatMap(Arg.Any<string>())
                .Returns(classificationFormatMap);

            var environmentService = Utils.CreateEnvironmentService(classificationFormatMapService);
            var actualTheme = environmentService.GetTheme();
            actualTheme.Should().Be(expectedTheme);
        }

        public static IEnumerable<Object[]> CreateTestData()
        {
            var defaultTheme = Theme.Light;

            yield return new Object[] { null, null, defaultTheme };
            yield return new Object[] { null, new RadialGradientBrush(), defaultTheme };
            yield return new Object[] { new RadialGradientBrush(), null, defaultTheme };
            yield return new Object[] { new RadialGradientBrush(), new RadialGradientBrush(), defaultTheme };

            yield return new Object[] { new SolidColorBrush(Colors.Black), null, Theme.Light };
            yield return new Object[] { null, new SolidColorBrush(Colors.White), Theme.Light };
            yield return new Object[] { new SolidColorBrush(Colors.Black), new SolidColorBrush(Colors.White), Theme.Light };

            yield return new Object[] { new SolidColorBrush(Colors.White), null, Theme.Dark };
            yield return new Object[] { null, new SolidColorBrush(Colors.Black), Theme.Dark };
            yield return new Object[] { new SolidColorBrush(Colors.White), new SolidColorBrush(Colors.Black), Theme.Dark };

            yield return new Object[] { new SolidColorBrush(Colors.Black), new RadialGradientBrush(), Theme.Light };
            yield return new Object[] { new RadialGradientBrush(), new SolidColorBrush(Colors.White), Theme.Light };

            yield return new Object[] { new SolidColorBrush(Colors.White), new RadialGradientBrush(), Theme.Dark };
            yield return new Object[] { new RadialGradientBrush(), new SolidColorBrush(Colors.Black), Theme.Dark };
        }
    }
}