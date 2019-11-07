using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[ExecuteInEditMode]
public class MotionIndicator : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI TMP_Text;

    [SerializeField] private Color Color_Motion;
    [SerializeField] private Color Color_Pause;
    [SerializeField] private Color Color_Park;
    [SerializeField] private Color Color_Transit;
    [SerializeField] private Color Color_Crashed;

    [SerializeField] private StartStopLogic startstoplogic;
   
    private void Start()
    {
        image = GetComponent<Image>();

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
    
    
    //Receiving Events
    public void OnCrashDetected()
    {
        Debug.Log("Motionindicator schaltet Licht auf rot");
    }
}
