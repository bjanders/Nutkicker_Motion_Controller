using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StartStopStatus
{
    Motion,
    Pause,
    Park,
    Transit
}

[ExecuteInEditMode]
public class StartStopLogic : MonoBehaviour
{
    //define events:
    [Serializable] public class StartStopChangedEvent : UnityEvent<StartStopStatus> { }
    //instantiate events:
    [SerializeField] public StartStopChangedEvent StartStopChanged;

    [SerializeField] public Platform platform_Pause;
    [SerializeField] public Platform platform_Physical;
    [Space]
    [SerializeField] public GameObject CoR;
    [SerializeField] public StartStopStatus Logicstatus;
    [SerializeField] public float transitionTime = 5.0f;
    [SerializeField] public CrashDetector crashDetector;

    

    private Lerp2Target lerpPause;
    private Lerp2Target lerpPhysical;

    private void Start()
    {
        lerpPause = platform_Pause.GetComponent<Lerp2Target>();
        lerpPhysical = platform_Physical.GetComponent<Lerp2Target>();

        lerpPause.Percentage = 0.0f;
        lerpPhysical.Percentage = 0.0f;

        Logicstatus = StartStopStatus.Park;
    }
  
    //Receive events:
    public void OnClick_Park2Pause()
    {
        if (Logicstatus == StartStopStatus.Park)
        {
            StartCoroutine(Park2Pause(lerpPause));
        }
    }
    public void OnClick_Pause2Motion()
    {
        if (Logicstatus == StartStopStatus.Pause)
        {
            if (crashDetector.Crashed)
            {
                Debug.LogError("Crash still present. Clear before starting motion");
                return;
            }
            StartCoroutine(Pause2Motion(lerpPhysical));
        }
    }
    public void OnClick_Motion2Pause()
    {
        if (Logicstatus == StartStopStatus.Motion)
        {
            StartCoroutine(Motion2Pause(lerpPhysical));
        }
    }
    public void OnClick_Pause2Park()
    {
        if (Logicstatus == StartStopStatus.Pause)
        {
            StartCoroutine(Pause2Park(lerpPause));
        }
    }
    

    ///////////---COROUTINES---//////////////
    IEnumerator Park2Pause(Lerp2Target lerp)
    {
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;

        Logicstatus = StartStopStatus.Pause;
        StartStopChanged.Invoke(Logicstatus);
    }
    IEnumerator Pause2Motion(Lerp2Target lerp)
    {
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;

        CoR.SetActive(true);

        Logicstatus = StartStopStatus.Motion;
        StartStopChanged.Invoke(Logicstatus);
    }
    IEnumerator Motion2Pause(Lerp2Target lerp)
    {
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);

        CoR.SetActive(false);

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;

        Logicstatus = StartStopStatus.Pause;
        StartStopChanged.Invoke(Logicstatus);
    }
    IEnumerator Pause2Park(Lerp2Target lerp)
    {
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;

        Logicstatus = StartStopStatus.Park;
        StartStopChanged.Invoke(Logicstatus);
    }
    
}