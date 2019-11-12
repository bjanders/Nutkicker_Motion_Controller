using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartStopStatus
{
    Motion,
    Pause,
    Park,
    Transit,
    Crashed
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
<<<<<<< HEAD
=======
    [SerializeField] public CrashDetector crashDetector;

    
>>>>>>> parent of 1edfc19... revert

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
    public void OnClick_Motion2Pause()
    {
<<<<<<< HEAD
        if (SwitchStatus == StartStopStatus.Motion)
=======
        if (Logicstatus == StartStopStatus.Park)
>>>>>>> parent of 1edfc19... revert
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
    public void OnClick_Park2Pause()
    {
<<<<<<< HEAD
        if (SwitchStatus == StartStopStatus.Park)
=======
        if (Logicstatus == StartStopStatus.Motion)
>>>>>>> parent of 1edfc19... revert
        {
            StartCoroutine(Park2Pause(lerpPause));
        }
    }
    public void OnClick_Pause2Motion()
    {
        if (Logicstatus == StartStopStatus.Pause)
        {
            StartCoroutine(Pause2Motion(lerpPhysical));
        }
    }
    public void OnCrashDetected()
    {
        SwitchStatus = StartStopStatus.Crashed;     //that means that no buttons will react anymore! Will only regain functionality after reset
    }
    public void OnCrashReset()
    {
        Debug.Log("RESET performed");
        SwitchStatus = StartStopStatus.Motion;      //Re-enable platform Motion..
        OnClick_Motion2Pause();                     //...and go to the Pause-position
    }

    ///////////---COROUTINES---//////////////
    IEnumerator Motion2Pause(Lerp2Target lerp)
    {
<<<<<<< HEAD
        while (lerp.Percentage > 0)
=======
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);

        while (lerp.Percentage < 1)
>>>>>>> parent of 1edfc19... revert
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
        lerp.Percentage = 0;

<<<<<<< HEAD
        SwitchStatus = StartStopStatus.Pause;
=======
        Logicstatus = StartStopStatus.Pause;
        StartStopChanged.Invoke(Logicstatus);
>>>>>>> parent of 1edfc19... revert
    }
    IEnumerator Pause2Park(Lerp2Target lerp)
    {
<<<<<<< HEAD
        SwitchStatus = StartStopStatus.Transit;
        CoR.SetActive(false);
=======
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);
>>>>>>> parent of 1edfc19... revert

        while (lerp.Percentage > 0)
        {
            lerp.Percentage -= (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
<<<<<<< HEAD
        lerp.Percentage = 0;
        SwitchStatus = StartStopStatus.Park;
=======
        lerp.Percentage = 1;

        CoR.SetActive(true);

        Logicstatus = StartStopStatus.Motion;
        StartStopChanged.Invoke(Logicstatus);
>>>>>>> parent of 1edfc19... revert
    }
    IEnumerator Park2Pause(Lerp2Target lerp)
    {
<<<<<<< HEAD
        SwitchStatus = StartStopStatus.Transit;
=======
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);

        CoR.SetActive(false);
>>>>>>> parent of 1edfc19... revert

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
<<<<<<< HEAD
        lerp.Percentage = 1;
        CoR.SetActive(true);
        SwitchStatus = StartStopStatus.Pause;
=======
        lerp.Percentage = 0;

        Logicstatus = StartStopStatus.Pause;
        StartStopChanged.Invoke(Logicstatus);
>>>>>>> parent of 1edfc19... revert
    }
    IEnumerator Pause2Motion(Lerp2Target lerp)
    {
<<<<<<< HEAD
        SwitchStatus = StartStopStatus.Motion;
=======
        Logicstatus = StartStopStatus.Transit;
        StartStopChanged.Invoke(Logicstatus);
>>>>>>> parent of 1edfc19... revert

        while (lerp.Percentage < 1)
        {
            lerp.Percentage += (1.0f / transitionTime) * Time.deltaTime;

            yield return null;
        }
<<<<<<< HEAD
        lerp.Percentage = 1;
=======
        lerp.Percentage = 0;

        Logicstatus = StartStopStatus.Park;
        StartStopChanged.Invoke(Logicstatus);
>>>>>>> parent of 1edfc19... revert
    }
}