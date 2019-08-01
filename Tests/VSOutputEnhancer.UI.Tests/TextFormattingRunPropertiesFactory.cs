using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Formatting;

namespace Balakin.VSOutputEnhancer.UI.Tests
{
    [ExcludeFromCodeCoverage]
    public class TextFormattingRunPropertiesFactory
    {
        public TextFormattingRunProperties Create(Color? foreground, Color? background)
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

            return Create(foregroundBrush, backgroundBrush);
        }

        public TextFormattingRunProperties Create(Brush foreground, Brush background)
        {
            var foregroundField = typeof(TextFormattingRunProperties).GetField("_foregroundBrush", BindingFlags.Instance | BindingFlags.NonPublic);
            var backgroundField = typeof(TextFormattingRunProperties).GetField("_backgroundBrush", BindingFlags.Instance | BindingFlags.NonPublic);

            var result = (TextFormattingRunProperties)Activator.CreateInstance(typeof(TextFormattingRunProperties), true);
            foregroundField.SetValue(result, foreground);
            backgroundField.SetValue(result, background);
            return result;
        }
    }
}