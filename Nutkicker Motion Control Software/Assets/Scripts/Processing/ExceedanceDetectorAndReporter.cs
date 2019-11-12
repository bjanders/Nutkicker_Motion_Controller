/* 
 * This Class is responsible for detecting a threshold exceedance in a datastream. Whenever an exceedance is present, it will...
 * 
 * 1. Block the downstream propagation of the exceedance value
 * 2. Send the last value that was present before the exceedance ocurred downstream instead
 * 3. Raise an "exceedance event" to alert the crash detector 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode]
public class ExceedanceDetectorAndReporter : MonoBehaviour
{
    //Define events:
    [Serializable] public class ExceedanceDetectedEvent : UnityEvent<float> { }
    //instatiate events:
    [SerializeField] public ExceedanceDetectedEvent exceedanceDetected;

    [SerializeField] Stream InStream;       //usuallly the parent
    [SerializeField] Stream OutStream;      //this is the Stream object attacherd to the gameobject 
    [Space]
    [SerializeField] StartStopLogic startstoplogic;
    [SerializeField] float CurrentValue;
    [SerializeField] public float Threshold;
    [SerializeField] float ValueBeforeLatch;  
    [SerializeField] float ValueTriggeringLatch;  
    [SerializeField] float PeakValue;  
    [SerializeField] Image RedDotMarker;  
    [SerializeField] public bool ExceedancePresent = false;
    [SerializeField] public bool latched = false;

<<<<<<< HEAD
    [SerializeField] public MyEvents.ExceedanceDetected exceedanceDetected;
=======
    
>>>>>>> parent of 1edfc19... revert

    private void Start()
    {
        if (transform.parent.gameObject.GetComponent<Stream>() != null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();
            this.name = InStream.name + "_PROT";
        }
        
        OutStream = GetComponent<Stream>();
    }
    void Update()
    {   //------------HIC SUNT DRACONES!!!!-------------
        CurrentValue = InStream.Youngest.Datavalue;
<<<<<<< HEAD
        UpdatePeakValue();                                          //for diagnoctics
        CheckForExceedance();

        RedDotMarker.enabled = ExceedancePresent;

        if (ExceedancePresent && !SignalLatched)                    //is this your first time here?
        {
            exceedanceDetected.Invoke(CurrentValue);                //raise the event once(!) --> Will be re-raised when unlatched while exceedance present!
            ValueTriggeringLatch = CurrentValue;                    //remember the value that triggered the latch
            SignalLatched = true;                                   //this will keep the exceedance detector in a latched state, can only be unlatched by calling the "OnCrashReset()" function.
        }

        if (SignalLatched)                                          //this can only be reached, if the exceedance is no longer present, but there WAS one in the past
        {
            OutStream.Push(new Datapoint(Time.fixedTime, ValueBeforeLatch, InStream.Type));                //we still send the last "safe" value
=======

        if (CheckForExceedance())
        {
            ExceedancePresent = true;
            latched = true;                                 //this will keep the exceedance detector in a latched state, can only be unlatched by calling the "OnCrashReset()" function.

            exceedanceDetected.Invoke(CurrentValue);        //raise the event

            OutStream.Push(new Datapoint(Time.fixedTime, ValueBeforeImpact, InStream.Type));
            ValueOnImpact = CurrentValue;
            return;
        }
        if (latched)                                        //this can only be reached, if the exceedance is no longer present
        {
            ExceedancePresent = false;
            OutStream.Push(new Datapoint(Time.fixedTime, ValueBeforeImpact, InStream.Type));        //we still send the last "safe" value
            return;
>>>>>>> parent of 2d1cf1b... Crash protection implemented (still messy)
        }
<<<<<<< HEAD
        else                                                //this is the most frequent case :-)
=======
        else                                                        //Normal ops here
>>>>>>> parent of 1edfc19... revert
        {
<<<<<<< HEAD
            ValueBeforeLatch = CurrentValue;                        //remember the last good value
<<<<<<< HEAD
=======
            ExceedancePresent = false;
            ValueBeforeImpact = CurrentValue;                   //remember, just in case
>>>>>>> parent of 2d1cf1b... Crash protection implemented (still messy)
=======
            
>>>>>>> parent of 1edfc19... revert
            OutStream.Push(new Datapoint(Time.fixedTime, CurrentValue, InStream.Type));
        }

        
    }
    private void CheckForExceedance()
    {
        if (Mathf.Abs(CurrentValue) >= Threshold)      
        {
            ExceedancePresent = true;                               //We have a crash!
        }
        else
        {
            ExceedancePresent = false;                              //All is fine
        }                                
    }
<<<<<<< HEAD

    //Events to receive:
=======
    
    //Events to receive:
>>>>>>> parent of 2d1cf1b... Crash protection implemented (still messy)
    public void OnThresholdChanged(float th)
    {
        Threshold = th;
    }
    public void Unlatch()                                           //this WILL unlatch the Exceedance detector! Might immediately re-trigger!!!
    {
<<<<<<< HEAD
        SignalLatched = false;
    }
    public void LatchCurrentValue()
    {
        SignalLatched = true;
    }
    public void UpdatePeakValue()
    {
        if (Mathf.Abs(CurrentValue) > Mathf.Abs(PeakValue))
        {
            PeakValue = CurrentValue;
        }
    }
<<<<<<< HEAD
=======
        latched = false;                //called by the Start/Stop logic. IT must ensure if this is save!s
    }






>>>>>>> parent of 2d1cf1b... Crash protection implemented (still messy)
=======

>>>>>>> parent of 1edfc19... revert
}
