using System;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] public bool Crashed;
    [SerializeField] public MyEvents.CrashDetectedEvent crashDetected;

    //events to receive:
    public void OnExceedanceDetected(float f)
    {
        Crashed = true;
        crashDetected.Invoke();
    }
    public void OnCrashReset()
    {
        Crashed = false;
    }
}
