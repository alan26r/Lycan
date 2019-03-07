using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    class TimeEvent
    {
        public float TimeToExecute;
        public Callback Method;
    }

    List<TimeEvent> events;

    public delegate void Callback();

    private void Awake()
    {
        events = new List<TimeEvent>();
    }

    public void Add(Callback method, float inSeconds)
    {
        events.Add(new TimeEvent
        {
            Method = method,
            TimeToExecute = Time.time + inSeconds
        });
    }

    public void Update()
    {
        if(events.Count == 0)
        {
            return;
        }
        for(int i = 0; i < events.Count; i++)
        {
            var timedEvent = events[i];
            if(timedEvent.TimeToExecute <= Time.time)
            {
                timedEvent.Method();
                events.Remove(timedEvent);
            }
        }
    }
}
