using System;
using UnityEngine;
using UnityEngine.Events;

public class MyEvents : MonoBehaviour
{
    [Serializable] public class ExceedanceDetectedEvent : UnityEvent<float> { }
    [Serializable] public class ExceedanceGoneEvent : UnityEvent { }
    [Serializable] public class CrashDetectedEvent : UnityEvent { }
    [Serializable] public class StartStopChangedEvent : UnityEvent<StartStopStatus> { }
    [Serializable] public class StartStopCrashRecoveredEvent : UnityEvent { }
}
