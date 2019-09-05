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
public class StartStopLogic : MonoBehaviour
{
    [SerializeField] public Platform platform_Pause;
    [SerializeField] public Platform platform_Physical;
    [SerializeField] public GameObject CoR;
    [SerializeField] public StartStopStatus SwitchStatus;
    [SerializeField] public float transitionTime = 5.0f;

    private Lerp2Target lerpPause;
    private Lerp2Target lerpPhysical;

    private void Start()
    {
        lerpPause = platform_Pause.GetComponent<Lerp2Target>();
        lerpPhysical = platform_Physical.GetComponent<Lerp2Target>();

        SwitchStatus = StartStopStatus.Park;
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
            StartCoroutine(Pause2Motion(lerpPhysical));
        }
    }

    ///////////---COROUTINES---//////////////
    IEnumerator Motion2Pause(Lerp2Target lerp)
    {
        //SwitchStatus = StartStopStatus.Transit;
        CoR.SetActive(false);

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;

        SwitchStatus = StartStopStatus.Pause;
    }
    IEnumerator Pause2Park(Lerp2Target lerp)
    {
        SwitchStatus = StartStopStatus.Transit;

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;
        SwitchStatus = StartStopStatus.Park;
    }
    IEnumerator Park2Pause(Lerp2Target lerp)
    {
        SwitchStatus = StartStopStatus.Transit;

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;
        SwitchStatus = StartStopStatus.Pause;
    }
    IEnumerator Pause2Motion(Lerp2Target lerp)
    {
        SwitchStatus = StartStopStatus.Motion;

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 1;

        CoR.SetActive(true);

        //SwitchStatus = StartStopStatus.Motion;
    }

}