using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Balakin.VSOutputEnhancer.Logic;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class StyleManagerTests
    {
        [Theory]
        [InlineData(Theme.Light)]
        [InlineData(Theme.Dark)]
        public void StylesAreSuccessfullyLoadedFromJson(Theme theme)
        {
            var styleManager = Utils.CreateStyleManager(theme);

            var stylesProperty = styleManager.GetType().GetProperty("Styles", BindingFlags.Instance | BindingFlags.NonPublic);
            var styles = (IDictionary<String, FormatDefinitionStyle>) stylesProperty.GetGetMethod(true).Invoke(styleManager, new Object[0]);
            styles.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(Theme.Light)]
        [InlineData(Theme.Dark)]
        public void SimilarClassificationTypesHaveSimilarColors(Theme theme)
        {
            var styleManager = Utils.CreateStyleManager(theme);

            var error = new[]
            {
                ClassificationType.BuildMessageError,
                ClassificationType.BuildResultFailed,
                ClassificationType.BuildStartedError,
                ClassificationType.PublishResultFailed,
                ClassificationType.DebugTraceError,
                ClassificationType.DebugException,
                ClassificationType.NpmResultFailed,
                ClassificationType.NpmMessageError,
                ClassificationType.BowerMessageError
            };
            var warning = new[]
            {
                ClassificationType.BuildStartedWarning,
                ClassificationType.BuildMessageWarning,
                ClassificationType.DebugTraceWarning,
                ClassificationType.NpmMessageWarning
            };
            var success = new[]
            {
                ClassificationType.BuildResultSucceeded,
                ClassificationType.BuildStartedSuccess,
                ClassificationType.PublishResultSucceeded,
                ClassificationType.NpmResultSucceeded
            };
            var skip = new[]
            {
                ClassificationType.DebugTraceInformation
            };

            TestSimilarColors(error.Select(styleManager.GetStyleForClassificationType).ToList(), "error");
            TestSimilarColors(warning.Select(styleManager.GetStyleForClassificationType).ToList(), "warning");
            TestSimilarColors(success.Select(styleManager.GetStyleForClassificationType).ToList(), "success");

            var notChecked = ClassificationType.All
                .Except(error)
                .Except(warning)
                .Except(success)
                .Except(skip);
            notChecked.Should().BeEmpty("All classification type styles should be checked");
        }

        private void TestSimilarColors(ICollection<FormatDefinitionStyle> styles, String groupName)
        {
            var foregroundColors = styles.Select(s => s.ForegroundColor).Distinct();
            foregroundColors.Should().HaveCount(1, "All styles should have the same color");
        }

        [Fact]
        public void UnknownClassificationStyle()
        {
            var expectedResult = new FormatDefinitionStyle
            {
                Bold = null,
                ForegroundColor = null
            };

            var styleManager = Utils.CreateStyleManager(Theme.Light);
            var style = styleManager.GetStyleForClassificationType("UnknownClassification");
            style.Should().BeEquivalentTo(expectedResult);
        }
    }
}