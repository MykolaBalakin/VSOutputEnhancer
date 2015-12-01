using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ColorExtensionsTests {
        [TestMethod]
        public void GetLightness() {
            TestLightness(0x990060, 0.3);
            TestLightness(0xEF3DB3, 0.588);
            TestLightness(0x66AB97, 0.535);
            TestLightness(0xA47C84, 0.565);
            TestLightness(0xEA2D8B, 0.547);
            TestLightness(0x1DF2A8, 0.531);
            TestLightness(0xE76C1D, 0.51);
            TestLightness(0x2531C9, 0.467);
            TestLightness(0xE5C008, 0.465);
            TestLightness(0x5787AE, 0.512);
        }

        private void TestLightness(Int32 color, Double expectedLightness) {
            var wpfColor = Color.FromRgb((Byte)(color >> 16 & 0xFF), (Byte)(color >> 8 & 0xFF), (Byte)(color & 0xFF));
            var lightness = wpfColor.GetLightness();
            Assert.AreEqual(expectedLightness, lightness, 0.001, "Color: " + wpfColor);
        }
    }
}
