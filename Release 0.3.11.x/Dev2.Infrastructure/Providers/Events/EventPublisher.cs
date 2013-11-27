﻿using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace Dev2.Providers.Events
{
    public class EventPublisher : IEventPublisher
    {
        readonly ConcurrentDictionary<Type, object> _subjects = new ConcurrentDictionary<Type, object>();
        readonly MethodInfo _publishMethod;

        public EventPublisher()
        {
            _publishMethod = typeof(EventPublisher).GetMethod("Publish");

        }

        public int Count { get { return _subjects.Count; } }

        public bool RemoveEvent<TEvent>()
            where TEvent : class, new()
        {
            object value;
            return _subjects.TryRemove(typeof(TEvent), out value);
        }

        public IObservable<TEvent> GetEvent<TEvent>()
            where TEvent : class, new()
        {
            var subject = (ISubject<TEvent>)_subjects.GetOrAdd(typeof(TEvent), t => new Subject<TEvent>());
            return subject.AsObservable();
        }

        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="sampleEvent">The event to be published</param>
        public void Publish<TEvent>(TEvent sampleEvent)
            where TEvent : class, new()
        {
            VerifyArgument.IsNotNull("sampleEvent", sampleEvent);

            object subject;
            if(_subjects.TryGetValue(typeof(TEvent), out subject))
            {
                ((ISubject<TEvent>)subject).OnNext(sampleEvent);
            }
        }

        /// <summary>
        /// Uses reflection to invoke <see cref="Publish{TEvent}"/>
        /// </summary>
        /// <param name="sampleEvent">The event to be published</param>
        public void PublishObject(object sampleEvent)
        {
            VerifyArgument.IsNotNull("sampleEvent", sampleEvent);

            var pubMethod = _publishMethod.MakeGenericMethod(sampleEvent.GetType());

            pubMethod.Invoke(this, new[] { sampleEvent });
        }
    }
}
