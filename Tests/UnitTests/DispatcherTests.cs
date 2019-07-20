using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
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

            public void Handle(IDispatcher dispatcher, DataContainer data, TEvent @event)
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
            var data = new DataContainer();
            dispatcher.Dispatch(@event, data);

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
            var data = new DataContainer();
            dispatcher.Dispatch(@event, data);

            handler.InvocationCount.Should().Be(1);
            anotherHandler.InvocationCount.Should().Be(0);
        }
    }
}