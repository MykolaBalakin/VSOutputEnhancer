using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Balakin.VSOutputEnhancer.Logic
{
    public class Dispatcher : IDispatcher
    {
        private readonly IDictionary<Type, Action<Object>> eventHandlers = new Dictionary<Type, Action<Object>>();

        public void AddHandler(IEventHandler handler)
        {
            foreach (var eventType in EnumerateEventTypes(handler))
            {
                AddEventHandler(handler, eventType);
            }
        }

        public void Dispatch(IEvent @event)
        {
            var type = @event.GetType();
            while (type != null)
            {
                InvokeHandlers(@event, type);
                type = type.BaseType;
            }
        }

        private IEnumerable<Type> EnumerateEventTypes(IEventHandler handler)
        {
            var eventHandlerInterface = typeof(IEventHandler<>);
            var allInterfaces = handler.GetType().GetInterfaces();
            return allInterfaces
                .Where(i => i.IsGenericType)
                .Where(i => i.GetGenericTypeDefinition() == eventHandlerInterface)
                .Select(i => i.GetGenericArguments().Single());
        }

        private void AddEventHandler(IEventHandler handler, Type eventType)
        {
            var genericMethodDefinition = typeof(Dispatcher)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(m => m.Name == nameof(AddEventHandler))
                .Single(m => m.IsGenericMethodDefinition);

            var genericMethod = genericMethodDefinition.MakeGenericMethod(eventType);
            genericMethod.Invoke(this, new Object[] { handler });
        }

        private void AddEventHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            Action<Object> handlerDelegateToAdd = (@event) => handler.Handle((TEvent)@event);
            if (eventHandlers.TryGetValue(typeof(TEvent), out var handlerDelegate))
            {
                handlerDelegateToAdd = handlerDelegate + handlerDelegateToAdd;
            }

            eventHandlers[typeof(TEvent)] = handlerDelegateToAdd;
        }

        private void InvokeHandlers(Object @event, Type eventType)
        {
            if (eventHandlers.TryGetValue(eventType, out var handler))
            {
                handler(@event);
            }
        }
    }
}