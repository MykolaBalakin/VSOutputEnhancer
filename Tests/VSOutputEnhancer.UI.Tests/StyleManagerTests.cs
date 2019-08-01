using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Balakin.VSOutputEnhancer.Logic;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Balakin.VSOutputEnhancer.UI.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class StyleManagerTests
    {
        [Theory]
        [InlineData(Theme.Light)]
        [InlineData(Theme.Dark)]
        public void StylesAreSuccessfullyLoadedFromJson(Theme theme)
        {
            var styleManager = CreateStyleManager(theme);

            var stylesField = styleManager.GetType().GetField("styles", BindingFlags.Instance | BindingFlags.NonPublic);
            var styles = (Lazy<IDictionary<String, FormatDefinitionStyle>>) stylesField.GetValue(styleManager);
            styles.Value.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(Theme.Light)]
        [InlineData(Theme.Dark)]
        public void SimilarClassificationTypesHaveSimilarColors(Theme theme)
        {
            var styleManager = CreateStyleManager(theme);

            var error = new[]
            {
                ClassificationType.BuildMessageError,
                ClassificationType.BuildResultFailed,
                ClassificationType.PublishResultFailed,
                ClassificationType.DebugTraceError,
                ClassificationType.DebugException,
                ClassificationType.NpmResultFailed,
                ClassificationType.NpmMessageError,
                ClassificationType.BowerMessageError
            };
            var warning = new[]
            {
                ClassificationType.BuildMessageWarning,
                ClassificationType.DebugTraceWarning,
                ClassificationType.NpmMessageWarning
            };
            var success = new[]
            {
                ClassificationType.BuildResultSucceeded,
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
            foregroundColors.Should().HaveCount(1, $"All styles in group \"{groupName}\" should have the same color");
        }

        [Fact]
        public void UnknownClassificationTypeHasDefaultStyle()
        {
            var expectedResult = new FormatDefinitionStyle
            {
                Bold = null,
                ForegroundColor = null
            };

            var styleManager = CreateStyleManager(Theme.Light);
            var style = styleManager.GetStyleForClassificationType("UnknownClassification");
            style.Should().BeEquivalentTo(expectedResult);
        }

        private IStyleManager CreateStyleManager(Theme theme)
        {
            var environmentService = Substitute.For<IEnvironmentService>();
            environmentService.GetTheme().Returns(theme);
            var styleManager = new StyleManager(environmentService);
            return styleManager;
        }
    }
}