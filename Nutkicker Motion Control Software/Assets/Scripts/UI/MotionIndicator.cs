using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class MotionIndicator : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI TMP_Text;
    [SerializeField] public StartStopLogic startstoplogic;
    [SerializeField] public CrashDetector crashdetector;
    [SerializeField] public EventTrigger eventtrigger;

    [SerializeField] private Color Color_Motion;
    [SerializeField] private Color Color_Pause;
    [SerializeField] private Color Color_Park;
    [SerializeField] private Color Color_Transit;
    [SerializeField] private Color Color_Crashed;
   
    private void Start()
    {
        image = GetComponent<Image>();
        eventtrigger = GetComponent<EventTrigger>();
        eventtrigger.enabled = false;
    }
    //Receiving Events
    public void OnCrashDetected()          //...by the crashdetector
    {
        eventtrigger.enabled = true;
        image.color = Color_Crashed;
        TMP_Text.text = "CRASH DETECTED - Push to reset";
    }
    public void OnCrashResetPushed()          //the button was clicked
    {
        StartCoroutine(LetRigSettle(3000));
        
    }
    public void OnStartStopLogicChanged(StartStopStatus status)
    {
        //Abnormals?
        if (crashdetector.Crashed)
        {
            eventtrigger.enabled = true;
            image.color = Color_Crashed;
            TMP_Text.text = "UNABLE MOTION due Crash";
            return;
        }
        //Normal Ops:
        eventtrigger.enabled = false;
        switch (status)
        {
            case StartStopStatus.Motion:
                eventtrigger.enabled = false;
                image.color = Color_Motion;
                TMP_Text.text = "--> MOTION ACTIVE <--";
                break;
            case StartStopStatus.Pause:
                eventtrigger.enabled = false;
                image.color = Color_Pause;
                TMP_Text.text = "Paused";
                break;
            case StartStopStatus.Park:
                image.color = Color_Park;
                TMP_Text.text = "Parked";
                break;
            case StartStopStatus.Transit:
                image.color = Color_Transit;
                TMP_Text.text = "In Transit";
                break;
            default:
                break;
        }
    }
    
    IEnumerator LetRigSettle(int ms)
    {
        image.color = Color_Transit;
        TMP_Text.text = "WAIT - Cleaning Filters";

        yield return new WaitForSeconds((float)ms / 1000);

        SetLightAccordingLogic();
    }
    void SetLightAccordingLogic()
    {
        OnStartStopLogicChanged(startstoplogic.SwitchStatus);       //Kinda like faking an event :-)
    }
}
