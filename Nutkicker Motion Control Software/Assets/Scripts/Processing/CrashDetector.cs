using System;
using UnityEngine;
using UnityEngine.Events;

public class CrashDetector : MonoBehaviour
{
    //define Events:
    [Serializable] public class CrashDetectedEvent : UnityEvent { }

    //instatiate events:
    [SerializeField] public bool Crashed;
    [SerializeField] public CrashDetectedEvent crashDetected;

    //events to receive:
    public void OnExceedanceDetected(float f)
    {
        Crashed = true;
        crashDetected.Invoke();                         //this latches ALL(!) exceedance detectors. It also informs the "CrashIndicatorAndReset" Object
    }
    public void OnCrashReset()
    {
        Crashed = false;
    }
}
