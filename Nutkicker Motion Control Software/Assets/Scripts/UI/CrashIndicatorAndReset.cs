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
    [Header("Excdeedance detectors")]
    [SerializeField] public ExceedanceDetectorAndReporter excd_Wx;
    [SerializeField] public ExceedanceDetectorAndReporter excd_Wy;
    [SerializeField] public ExceedanceDetectorAndReporter excd_Wz;
    [SerializeField] public ExceedanceDetectorAndReporter excd_Ax;
    [SerializeField] public ExceedanceDetectorAndReporter excd_Ay;
    [SerializeField] public ExceedanceDetectorAndReporter excd_Az;
    [Space]
    [SerializeField] public EventTrigger eventtrigger;
    [SerializeField] public int SettleTime;

    [SerializeField] private Color Color_Passive;
    [SerializeField] private Color Color_Draining;
    [SerializeField] private Color Color_Crashed;

    private ExceedanceDetectorAndReporter[] ExcDaR_Array;
   
    private void Start()
    {
        image = GetComponent<Image>();
        eventtrigger = GetComponent<EventTrigger>();
        eventtrigger.enabled = false;

        ExcDaR_Array = new ExceedanceDetectorAndReporter[6] { excd_Wx, excd_Wy, excd_Wz, excd_Ax, excd_Ay, excd_Az };     //makes checking them easier :-)

        image.color = Color_Passive;
        TMP_Text.text = "No Warning";
        TMP_Text.fontSize = 14;
    }

    //Receiving Events
    public void OnCrashDetected()               //...by the crashdetector
    {
        image.color = Color_Crashed;
        TMP_Text.text = "C R A S H   D E T E C T E D";
        TMP_Text.fontSize = 18;

        eventtrigger.enabled = true;            //The reset functionality becomes active
    }
    public void OnCrashResetPushed()            //the button was clicked
    {
        StartCoroutine(OrderlyReset());
    }

    IEnumerator OrderlyReset()
    {
        eventtrigger.enabled = false;                                           //No need to press again. Don't interfere while OrderlyReset() is in progress

        image.color = Color_Draining;
        TMP_Text.text = "Recovering Rig - Please Wait" ;

        while ( startstoplogic.Logicstatus == StartStopStatus.Motion ||         //We don't wanna be in those phases, so...
                startstoplogic.Logicstatus == StartStopStatus.Transit)
        {
            startstoplogic.OnClick_Motion2Pause();                              //...get out of it!
            yield return new WaitForSeconds(0.1f);                              //check every 100ms if plaform is in PAUSE or Park (out of MOTION or TRANSIT)
        }         

        while (!All_ExcDaR_NoExcd())                                            //check for persisting Exceedances AFTER the recovery is done!!! Might even be a re-trigger!
        {
            image.color = Color_Crashed;
            TMP_Text.text = "Crash STILL present! Reset Sim";
            yield return new WaitForSeconds(0.1f);                              //check every 100ms if exceedances are gone
        }

        //--------------ALL IS GOOD----------------
       
        UnlatchAllExcDaR();                                                     //this should be safe. After all, we checked for exceedances just a few nanoseconds ago. The Plat_Motion will now freefloat to it's "MotionActive" position. Plat_Physical can join it, when StartStopLogic Lerps her over.
        crashdetector.OnCrashReset();                                           //so it can detect the next crash

        image.color = Color_Draining;
        for (int counter = SettleTime; counter > 0; counter--)
        {
            TMP_Text.text = "Draining Filters - please wait: " + counter.ToString();
            yield return new WaitForSeconds(1);
        }

        image.color = Color_Passive;
        TMP_Text.text = "READY!";
        yield return new WaitForSeconds(2);                                     //Show the "READY" message for 2 seconds

        TMP_Text.text = "";
    }

    private bool All_ExcDaR_NoExcd()
    {
        bool flag = true;                                                       //Let's assume they're all OK :-)
        foreach (ExceedanceDetectorAndReporter ExcDaR in ExcDaR_Array)
        {
            if (ExcDaR.ExceedancePresent)                                       //...but better check!
            {
                flag = false;                                                   //Damn it! :-(
            }
        }
        return flag;
    }
    private bool All_ExcDaR_NoLatch()
    {
        bool flag = true;                                                       //Let's assume they're all un-latched :-)
        foreach (ExceedanceDetectorAndReporter ExcDaR in ExcDaR_Array)
        {
            if (ExcDaR.SignalLatched)                                           //...but better check!
            {
                flag = false;                                                   //Damn it! :-(
            }
        }
        return flag;
    }
    private void UnlatchAllExcDaR()
    {
        foreach (ExceedanceDetectorAndReporter ExcDaR in ExcDaR_Array)
        {
            ExcDaR.Unlatch();
        }
    }
}
