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
    [SerializeField] public Platform platform_Pause;
    [SerializeField] public Platform platform_Physical;
    [Space]
    [SerializeField] public GameObject CoR;
    [SerializeField] public StartStopStatus SwitchStatus;
    [SerializeField] public float transitionTime = 5.0f;
    [SerializeField] public CrashDetector crashDetector;

    [SerializeField] public MyEvents.StartStopChangedEvent StartStopChanged;
    [SerializeField] public MyEvents.StartStopCrashRecoveredEvent StartStopCrashRecovered;

    private Lerp2Target lerpPause;
    private Lerp2Target lerpPhysical;

    private void Start()
    {
        lerpPause = platform_Pause.GetComponent<Lerp2Target>();
        lerpPhysical = platform_Physical.GetComponent<Lerp2Target>();

        lerpPause.Percentage = 0.0f;
        lerpPhysical.Percentage = 0.0f;

        SwitchStatus = StartStopStatus.Park;
    }
  
    //Receive events:
    public void OnClick_Park2Pause()
    {
        if (SwitchStatus == StartStopStatus.Park)
        {
            StartCoroutine(Park2Pause(lerpPause));
        }
    }
    public void OnClick_Pause2Motion()
    {
        if (SwitchStatus == StartStopStatus.Pause)
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
        if (SwitchStatus == StartStopStatus.Motion)
        {
            StartCoroutine(Motion2Pause(lerpPhysical));
        }
    }
    public void OnClick_Pause2Park()
    {
        if (SwitchStatus == StartStopStatus.Pause)
        {
            StartCoroutine(Pause2Park(lerpPause));
        }
    }
    

    ///////////---COROUTINES---//////////////
    IEnumerator Park2Pause(Lerp2Target lerp)
    {
        SwitchStatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(SwitchStatus);

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;

        SwitchStatus = StartStopStatus.Pause;
        StartStopChanged.Invoke(SwitchStatus);
    }
    IEnumerator Pause2Motion(Lerp2Target lerp)
    {
        SwitchStatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(SwitchStatus);

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;

        CoR.SetActive(true);

        SwitchStatus = StartStopStatus.Motion;
        StartStopChanged.Invoke(SwitchStatus);
    }
    IEnumerator Motion2Pause(Lerp2Target lerp)
    {
        SwitchStatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(SwitchStatus);

        CoR.SetActive(false);

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;

        SwitchStatus = StartStopStatus.Pause;
        StartStopChanged.Invoke(SwitchStatus);
    }
    IEnumerator Pause2Park(Lerp2Target lerp)
    {
        SwitchStatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(SwitchStatus);

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;

        SwitchStatus = StartStopStatus.Park;
        StartStopChanged.Invoke(SwitchStatus);
    }
    
}