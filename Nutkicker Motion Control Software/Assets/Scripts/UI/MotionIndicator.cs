using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[ExecuteInEditMode]
public class MotionIndicator : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI TMP_Text;
<<<<<<< HEAD
    [SerializeField] public StartStopLogic startstoplogic;
=======
>>>>>>> parent of 2d1cf1b... Crash protection implemented (still messy)

    [SerializeField] private Color Color_Motion;
    [SerializeField] private Color Color_Pause;
    [SerializeField] private Color Color_Park;
    [SerializeField] private Color Color_Transit;
    [SerializeField] private Color Color_Crashed;

    [SerializeField] private StartStopLogic startstoplogic;
   
    private void Start()
    {
        image = GetComponent<Image>();
<<<<<<< HEAD
    }

    //Receiving Events
    public void OnCrashDetected()          //...by the crashdetector
    {
    }
    public void OnCrashResetPushed()          //the button was clicked
    {
    }
    public void OnStartStopLogicChanged(StartStopStatus status)
    {
        switch (status)
        {
            case StartStopStatus.Motion:
                image.color = Color_Motion;
                TMP_Text.text = "--> MOTION ACTIVE <--";
                break;
            case StartStopStatus.Pause:
                image.color = Color_Pause;
=======

        //Subscribe to events

    }
    void Update()
    {
        switch (startstoplogic.SwitchStatus)
        {
            case StartStopStatus.Motion:
                image.color = Color_Motion;
                TMP_Text.text = "--> MOTION ACTIVE <--";
                break;
            case StartStopStatus.Pause:
                image.color = Color_Pause;
>>>>>>> parent of 2d1cf1b... Crash protection implemented (still messy)
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
            case StartStopStatus.Crashed:
                image.color = Color_Crashed;
                TMP_Text.text = "CRASH DETECTED - Click to reset";
                break;
            default:
                break;
        }
    }
<<<<<<< HEAD
=======
    
    
    //Receiving Events
    public void OnCrashDetected()
    {
        Debug.Log("Motionindicator schaltet Licht auf rot");
    }
    public void OnCrashReset()
    {
        Debug.Log("Motionindicator schaltet Licht auf Grau");
    }
>>>>>>> parent of 2d1cf1b... Crash protection implemented (still messy)
}
