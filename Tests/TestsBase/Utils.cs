using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Media;
using Balakin.VSOutputEnhancer.Logic;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Formatting;

namespace Balakin.VSOutputEnhancer.Tests
{
    [ExcludeFromCodeCoverage]
    public static class Utils
    {
        public static SnapshotSpan CreateSpan(String text)
        {
            var snapshot = new TextSnapshotStub(text);
            return new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
        }

        public static ITextBuffer CreateTextBuffer(String contentType)
        {
            return new TextBufferStub(contentType);
        }

        public static IClassificationTypeRegistryService CreateClassificationTypeRegistryService()
        {
            return new ClassificationTypeRegistryServiceStub();
        }

        public static IClassificationTypeService CreateClassificationTypeService()
        {
            var classificationTypeRegistryService = Utils.CreateClassificationTypeRegistryService();
            return new ClassificationTypeService(classificationTypeRegistryService);
        }

        public static IStyleManager CreateStyleManager()
        {
            var styleManager = new StyleManagerStub();
            return styleManager;
        }

        public static IStyleManager CreateStyleManager(Theme theme)
        {
            var environmentService = new EnvironmentServiceStub(theme);
            var styleManager = new StyleManager(environmentService);
            return styleManager;
        }

        public static IEnvironmentService CreateEnvironmentService(IClassificationFormatMapService classificationFormatMapService)
        {
            var environmentService = new EnvironmentService(classificationFormatMapService);
            return environmentService;
        }

        private static Func<Brush, Brush, TextFormattingRunProperties> GetCreateTextFormattingRunProperties()
        {
            var foregroundParameter = Expression.Parameter(typeof(Brush), "foreground");
            var backgroundParameter = Expression.Parameter(typeof(Brush), "background");

            var type = typeof(TextFormattingRunProperties);

            var resultVariable = Expression.Variable(type, "result");
            var returnLabel = Expression.Label(type);

            var constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single(c => c.GetParameters().Length == 0);

            var expression = Expression.Lambda<Func<Brush, Brush, TextFormattingRunProperties>>(
                Expression.Block(
                    new[] { resultVariable },
                    Expression.Assign(resultVariable, Expression.New(constructor)),
                    Expression.Assign(Expression.Field(resultVariable, "_foregroundBrush"), foregroundParameter),
                    Expression.Assign(Expression.Field(resultVariable, "_backgroundBrush"), backgroundParameter),
                    Expression.Return(returnLabel, resultVariable, type),
                    Expression.Label(returnLabel, Expression.Default(type))
                ),
                foregroundParameter,
                backgroundParameter
            );

            return expression.Compile();
        }

        private static readonly Lazy<Func<Brush, Brush, TextFormattingRunProperties>> createTextFormattingRunProperties = new Lazy<Func<Brush, Brush, TextFormattingRunProperties>>(GetCreateTextFormattingRunProperties);

        public static TextFormattingRunProperties CreateTextFormattingRunProperties(Color? foreground, Color? background)
        {
            var foregroundBrush = (Brush) null;
            if (foreground.HasValue)
            {
                foregroundBrush = new SolidColorBrush(foreground.Value);
            }
            var backgroundBrush = (Brush) null;
            if (background.HasValue)
            {
                backgroundBrush = new SolidColorBrush(background.Value);
            }

            return Utils.CreateTextFormattingRunProperties(foregroundBrush, backgroundBrush);
        }

        public static TextFormattingRunProperties CreateTextFormattingRunProperties(Brush foreground, Brush background)
        {
            var result = createTextFormattingRunProperties.Value(foreground, background);
            return result;
        }
    }
}