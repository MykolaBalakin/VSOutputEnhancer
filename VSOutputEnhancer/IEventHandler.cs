using System;
using System.Collections.Generic;

namespace Balakin.VSOutputEnhancer
{
    public interface IEventHandler
    {
        IEnumerable<String> ContentTypes { get; }
    }

    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}