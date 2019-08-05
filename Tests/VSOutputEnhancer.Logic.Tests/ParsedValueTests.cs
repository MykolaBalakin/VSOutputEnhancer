using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ParsedValueTests
    {
        [Fact]
        public void EmptyValueThrowsExceptionValueType()
        {
            var value = new ParsedValue<Int32>();
            Func<Int32> action = () => (Int32) value;
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void EmptyValueThrowsExceptionReferenceType()
        {
            var value = new ParsedValue<String>();
            Func<String> action = () => (String) value;
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void CastToUnderlyingValueType()
        {
            var originalValue = 1;

            var parsedValue = new ParsedValue<Int32>(originalValue, new Span());
            var actualValue = (Int32) parsedValue;

            actualValue.Should().Be(originalValue);
        }

        [Fact]
        public void CastToUnderlyingReferenceType()
        {
            var originalValue = "test";

            var parsedValue = new ParsedValue<String>(originalValue, new Span());
            var actualValue = (String) parsedValue;

            actualValue.Should().Be(originalValue);
        }
    }
}