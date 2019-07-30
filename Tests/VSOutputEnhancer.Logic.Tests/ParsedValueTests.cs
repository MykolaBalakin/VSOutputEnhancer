using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using FluentAssertions;
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
    }
}