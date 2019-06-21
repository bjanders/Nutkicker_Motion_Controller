using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartStopStatus
{
    Motion,
    Pause,
    Park,
    Transit
}

[ExecuteInEditMode]
public class StartStopSwitch : MonoBehaviour
{
    [SerializeField] public Platform platform_Pause;
    [SerializeField] public Platform platform_Physical;
    [SerializeField] public StartStopStatus status;
    [SerializeField] public float transitionTime = 5.0f;

    private LerpToPosition lerpPause;
    private LerpToPosition lerpPhysical;

    private void Start()
    {
        lerpPause = platform_Pause.GetComponent<LerpToPosition>();
        lerpPhysical = platform_Physical.GetComponent<LerpToPosition>();

        status = StartStopStatus.Park;
    }

    public void OnClick_Motion2Pause()
    {
        if (status == StartStopStatus.Motion)
        {
            StartCoroutine(Motion2Pause(lerpPhysical));
        }
    }
    public void OnClick_Pause2Park()
    {
        if (status == StartStopStatus.Pause)
        {
            StartCoroutine(Pause2Park(lerpPause));
        }
    }
    public void OnClick_Park2Pause()
    {
        if (status == StartStopStatus.Park)
        {
            StartCoroutine(Park2Pause(lerpPause));
        }
    }
    public void OnClick_Pause2Motion()
    {
        if (status == StartStopStatus.Pause)
        {
            StartCoroutine(Pause2Motion(lerpPhysical));
        }
    }

    ///////////---COROUTINES---//////////////
    IEnumerator Motion2Pause(LerpToPosition lerp)
    {
        status = StartStopStatus.Transit;

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;

        status = StartStopStatus.Pause;
    }
    IEnumerator Pause2Park(LerpToPosition lerp)
    {
        status = StartStopStatus.Transit;

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;
        status = StartStopStatus.Park;
    }
    IEnumerator Park2Pause(LerpToPosition lerp)
    {
        status = StartStopStatus.Transit;

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;
        status = StartStopStatus.Pause;
    }
    IEnumerator Pause2Motion(LerpToPosition lerp)
    {
        status = StartStopStatus.Transit;

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;

        status = StartStopStatus.Motion;
    }
   
}
