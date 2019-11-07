using System;
using UnityEngine;
using UnityEngine.Events;

public class MyEvents : MonoBehaviour
{
    [Serializable] public class ExceedanceDetected : UnityEvent<float> { }
    [Serializable] public class CrashDetected : UnityEvent { }
}
