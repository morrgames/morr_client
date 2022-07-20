using System;
using UniRx;
using UnityEngine;

public static class Observable
{
    static Subject<EventInfo> eventStream = new Subject<EventInfo>();

    public static Subject<EventInfo> EventAsObservable()
    {
        return eventStream;
    }

    public static IDisposable Subscribe(ObservableEvent.Event _event, Action<EventInfo> _onEvent)
    {
        return Observable.EventAsObservable()
            .Where(e => e.eventName == _event)
            .Subscribe(_onEvent);
    }

    public class EventInfo
    {
        public object[] args;
        public ObservableEvent.Event eventName;

        public EventInfo(ObservableEvent.Event eventName, params object[] args)
        {
            this.eventName = eventName;
            this.args = args;
        }
    }
}