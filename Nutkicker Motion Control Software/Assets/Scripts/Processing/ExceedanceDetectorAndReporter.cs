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
using UnityEngine.Events;

[ExecuteInEditMode]
public class ExceedanceDetectorAndReporter : MonoBehaviour
{
    [SerializeField] Stream InStream;       //usuallly the parent
    [SerializeField] Stream OutStream;      //this is the Stream object attacherd to the gameobject 
    [Space]
    [SerializeField] float CurrentValue;
    [SerializeField] public float Threshold;
    [SerializeField] float ValueBeforeImpact;  
    [SerializeField] float ValueOnImpact;  
    [SerializeField] public bool ExceedancePresent = false;
    [SerializeField] public bool latched = false;

    [SerializeField] public MyEvents.ExceedanceDetected exceedanceDetected;

    private void Start()
    {
        if (transform.parent.gameObject.GetComponent<Stream>() != null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();
            OutStream = GetComponent<Stream>();

            this.name = InStream.name + "_PROT";
        }
    }
    void Update()
    {
        CurrentValue = InStream.Youngest.Datavalue;

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
        }
        else                                                //this is the most frequent case :-)
        {
            ExceedancePresent = false;
            ValueBeforeImpact = CurrentValue;                   //remember, just in case
            OutStream.Push(new Datapoint(Time.fixedTime, CurrentValue, InStream.Type));
        }

        
    }
    private bool CheckForExceedance()
    {
        if (Mathf.Abs(CurrentValue) >= Threshold)      
        {
            return true;                                //We have a crash!
        }
        return false;                                  //All is fine
    }
    
    //Events to receive:
    public void OnThresholdChanged(float th)
    {
        Threshold = th;
    }
    public void OnCrashReset()
    {
        latched = false;                //called by the Start/Stop logic. IT must ensure if this is save!s
    }






}
