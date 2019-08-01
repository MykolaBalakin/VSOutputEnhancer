using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.UI.Tests
{
    [ExcludeFromCodeCoverage]
    public class ColorExtensionsTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void GetLightness(Int32 color, Double expectedLightness)
        {
            const Double precision = 0.001;

            var wpfColor = Color.FromRgb((Byte)(color >> 16 & 0xFF), (Byte)(color >> 8 & 0xFF), (Byte)(color & 0xFF));
            var actualLightness = wpfColor.GetLightness();
            actualLightness.Should().BeApproximately(expectedLightness, precision);
        }

        public static IEnumerable<Object[]> CreateTestData()
        {
            yield return new Object[] { 0x990060, 0.3 };
            yield return new Object[] { 0xEF3DB3, 0.588 };
            yield return new Object[] { 0x66AB97, 0.535 };
            yield return new Object[] { 0xA47C84, 0.565 };
            yield return new Object[] { 0xEA2D8B, 0.547 };
            yield return new Object[] { 0x1DF2A8, 0.531 };
            yield return new Object[] { 0xE76C1D, 0.51 };
            yield return new Object[] { 0x2531C9, 0.467 };
            yield return new Object[] { 0xE5C008, 0.465 };
            yield return new Object[] { 0x5787AE, 0.512 };
        }
    }
}