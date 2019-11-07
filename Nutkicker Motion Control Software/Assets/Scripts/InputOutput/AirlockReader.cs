 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

[ExecuteInEditMode]
public class AirlockReader : MonoBehaviour
{
    [SerializeField] public string RawDataString;

    private void Start()
    {
        RawDataString = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
    }

    void FixedUpdate()
    {
        if (Airlock.Container.TryTake(out string s))
        {
            RawDataString = s;
        }
    }
}   