using System;
using UnityEngine;
using UnityEngine.Events;

public class CrashDetector : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] public MyEvents.CrashDetected crashDetected;
=======
    //define Events:
    [Serializable] public class CrashDetectedEvent : UnityEvent { }

    //instatiate events:
    [SerializeField] public bool Crashed;
    [SerializeField] public CrashDetectedEvent crashDetected;
>>>>>>> parent of 1edfc19... revert

    //events to receive:
    public void OnExceedanceDetected(float f)
    {
<<<<<<< HEAD
        Debug.Log("Crash Detector received an exceedance of " + f.ToString());
        crashDetected.Invoke();
=======
        Crashed = true;
        crashDetected.Invoke();                         //this latches ALL(!) exceedance detectors. It also informs the "CrashIndicatorAndReset" Object
>>>>>>> parent of 1edfc19... revert
    }
}
