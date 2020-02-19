using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class DispatcherTests
    {
        private class TestEvent : IEvent
        {
        }

        private class AnotherTestEvent : IEvent
        {
        }

        private class TestEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
        {
            public IEnumerable<String> ContentTypes { get; }
            public Int32 InvocationCount { get; private set; }

            public void Handle(IDispatcher dispatcher, TEvent @event)
            {
                InvocationCount++;
            }
        }

        [Fact]
        public void AddHandlerDoesNotThrowException()
        {
            var dispatcher = new Dispatcher();
            var handler = new TestEventHandler<TestEvent>();
            dispatcher.AddHandler(handler);
        }

        [Fact]
        public void DispatchInvokesHandler()
        {
            var dispatcher = new Dispatcher();
            var handler = new TestEventHandler<TestEvent>();
            dispatcher.AddHandler(handler);

            var @event = new TestEvent();
            dispatcher.Dispatch(@event);

            handler.InvocationCount.Should().Be(1);
        }

        [Fact]
        public void DispatchInvokesOnlyAppropriateHandler()
        {
            var dispatcher = new Dispatcher();
            var handler = new TestEventHandler<TestEvent>();
            dispatcher.AddHandler(handler);

            var anotherHandler = new TestEventHandler<AnotherTestEvent>();
            dispatcher.AddHandler(anotherHandler);

            var @event = new TestEvent();
            dispatcher.Dispatch(@event);

            handler.InvocationCount.Should().Be(1);
            anotherHandler.InvocationCount.Should().Be(0);
        }

        [Fact]
        public void DispatchInvokesMultipleHandlers()
        {
            var dispatcher = new Dispatcher();

            var firstHandler = new TestEventHandler<TestEvent>();
            dispatcher.AddHandler(firstHandler);

            var secondHandler = new TestEventHandler<TestEvent>();
            dispatcher.AddHandler(secondHandler);

            var @event = new TestEvent();
            dispatcher.Dispatch(@event);

            firstHandler.InvocationCount.Should().Be(1);
            secondHandler.InvocationCount.Should().Be(1);
        }
    }
}