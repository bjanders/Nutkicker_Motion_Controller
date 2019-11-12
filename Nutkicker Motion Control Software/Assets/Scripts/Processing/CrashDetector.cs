using System;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] public MyEvents.CrashDetected crashDetected;

    //events to receive:
    public void OnExceedanceDetected(float f)
    {
        Debug.Log("Crash Detector received an exceedance of " + f.ToString());
        crashDetected.Invoke();
    }
}
