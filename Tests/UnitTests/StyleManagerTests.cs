using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StyleManagerTests {
        [TestMethod]
        public void SuccessfullyLoadedFromJson() {
            TestLoadedFromJson(Theme.Light);
            TestLoadedFromJson(Theme.Dark);
        }

        private void TestLoadedFromJson(Theme theme) {
            var styleManager = Utils.CreateStyleManager(theme);

            var stylesProperty = styleManager.GetType().GetProperty("Styles", BindingFlags.Instance | BindingFlags.NonPublic);
            var styles = (IDictionary<String, FormatDefinitionStyle>)stylesProperty.GetGetMethod(true).Invoke(styleManager, new Object[0]);
            Assert.IsTrue(styles.Count > 0, $"Styles for {theme} theme not loaded");
        }

        [TestMethod]
        public void SimilarClassificationTypesHasSimilarColors() {
            TestSimilarColors(Theme.Light);
            TestSimilarColors(Theme.Dark);
        }

        private void TestSimilarColors(Theme theme) {
            var styleManager = Utils.CreateStyleManager(theme);

            var error = new[] {
                ClassificationType.BuildMessageError,
                ClassificationType.BuildResultFailed,
                ClassificationType.PublishResultFailed,
                ClassificationType.DebugTraceError,
                ClassificationType.DebugException
            };
            var warning = new [] {
                ClassificationType.BuildMessageWarning,
                ClassificationType.DebugTraceWarning
            };
            var success = new [] {
                ClassificationType.BuildResultSucceeded,
                ClassificationType.PublishResultSucceeded
            };
            var skip = new[] {
                ClassificationType.DebugTraceInformation
            };

            TestSimilarColors(error.Select(styleManager.GetStyleForClassificationType).ToList(), "error");
            TestSimilarColors(warning.Select(styleManager.GetStyleForClassificationType).ToList(), "warning");
            TestSimilarColors(success.Select(styleManager.GetStyleForClassificationType).ToList(), "success");

            var notChecked = ClassificationType.All
                .Except(error)
                .Except(warning)
                .Except(success)
                .Except(skip)
                .ToList();
            Assert.AreEqual(0, notChecked.Count, "This classification types did not checked: " + String.Join(", ", notChecked));
        }

        private void TestSimilarColors(ICollection<FormatDefinitionStyle> styles, String groupName) {
            var foregroundColors = styles.Where(c => c.ForegroundColor.HasValue).Select(s => s.ForegroundColor.Value).Distinct().ToList();
            Assert.IsTrue(foregroundColors.Count <= 1, $"Some {groupName} styles has different colors");
        }

        [TestMethod]
        public void UnknownClassificationStyle() {
            var styleManager = Utils.CreateStyleManager(Theme.Light);
            var style = styleManager.GetStyleForClassificationType("UnknownClassification");
            Assert.IsNotNull(style);
            Assert.IsNull(style.Bold);
            Assert.IsNull(style.ForegroundColor);
        }
    }
}
