using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DetectCrash : MonoBehaviour
{
    [SerializeField] Stream InStream;
    [SerializeField] Stream OutStream;
    [Space]
    [SerializeField] float Threshold;
    [SerializeField] float DefaultValue;
    [SerializeField] float ValueOnImpact;  
    [SerializeField] public bool CrashDetected = false;

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
        CrashDetected = checkForCrash();

        if (CrashDetected)
        {
            OutStream.Push(new Datapoint(Time.fixedTime, DefaultValue, InStream.Type));
            return;
        }

        OutStream.Push(new Datapoint(Time.fixedTime, InStream.Youngest.Datavalue, InStream.Type));
    }

    private bool checkForCrash()
    {
        float value = InStream.Youngest.Datavalue;

        if (Mathf.Abs(value) >= Threshold)
        {
            ValueOnImpact = value;      //We have a crash!
            return true;        
        }
        return false;                   //All is fine
    }
}
