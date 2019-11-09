using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class CrashIndicatorAndReset : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI TMP_Text;
    [SerializeField] public StartStopLogic startstoplogic;
    [SerializeField] public CrashDetector crashdetector;
    [SerializeField] public EventTrigger eventtrigger;
    [SerializeField] public int SettleTime;

    [SerializeField] private Color Color_Passive;
    [SerializeField] private Color Color_Draining;
    [SerializeField] private Color Color_Crashed;
   
    private void Start()
    {
        image = GetComponent<Image>();
        eventtrigger = GetComponent<EventTrigger>();
        eventtrigger.enabled = false;

        image.color = Color_Passive;
        TMP_Text.text = "No Warning";
        TMP_Text.fontSize = 14;
    }

    //Receiving Events
    public void OnCrashDetected()          //...by the crashdetector
    {
        image.color = Color_Crashed;
        TMP_Text.text = "C R A S H   D E T E C T E D";
        TMP_Text.fontSize = 18;

        eventtrigger.enabled = true;
    }
    public void OnCrashResetPushed()          //the button was clicked
    {
        StartCoroutine(CalmReset());
    }

    IEnumerator CalmReset()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (crashdetector.Crashed)
        {
            image.color = Color_Crashed;
            TMP_Text.text = "Crash Still present! Reset Sim";
        }
        else
        {   
            eventtrigger.enabled = false;

            image.color = Color_Draining;

            int counter = SettleTime;
            while (counter > 0)
            {
                TMP_Text.text = "Draining Filters - please wait: " + counter.ToString();
                counter--;

                yield return new WaitForSeconds(1);
            }

            image.color = Color_Passive;
            TMP_Text.text = "READY!";
            yield return new WaitForSeconds(2);

            TMP_Text.text = "";
        }
    }
    

}
