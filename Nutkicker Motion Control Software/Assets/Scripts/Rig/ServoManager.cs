using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ServoManager : MonoBehaviour
{
    [SerializeField] Servo Servo1;
    [SerializeField] Servo Servo2;
    [SerializeField] Servo Servo3;
    [SerializeField] Servo Servo4;
    [SerializeField] Servo Servo5;
    [SerializeField] Servo Servo6;
    [Space]
    [SerializeField] Transform Cnx_Low1;
    [SerializeField] Transform Cnx_Low2;
    [SerializeField] Transform Cnx_Low3;
    [SerializeField] Transform Cnx_Low4;
    [SerializeField] Transform Cnx_Low5;
    [SerializeField] Transform Cnx_Low6;
    [Space]
    [SerializeField] Transform Cnx_Upr1;
    [SerializeField] Transform Cnx_Upr2;
    [SerializeField] Transform Cnx_Upr3;
    [SerializeField] Transform Cnx_Upr4;
    [SerializeField] Transform Cnx_Upr5;
    [SerializeField] Transform Cnx_Upr6;
    [Space]
    [SerializeField] public float azimuth;         //The Azimuth of Servo Nr.1
    [Range(0, 0.5f)]
    [SerializeField] public float crank_Length;    //ALL crank lenghts are equal. Set them here!
    [SerializeField] public float rod_Length;      //ALL rod lenghts are equal. Set them here!
    [SerializeField] public bool FlipCranks;   //Do you need the "other" solution?

    //----------------------------------------------------
    void Start()
    {
        Servo1.BottomJoint = Cnx_Low1;
        Servo2.BottomJoint = Cnx_Low2;
        Servo3.BottomJoint = Cnx_Low3;
        Servo4.BottomJoint = Cnx_Low4;
        Servo5.BottomJoint = Cnx_Low5;
        Servo6.BottomJoint = Cnx_Low6;

        Servo1.UpperJoint = Cnx_Upr1;
        Servo2.UpperJoint = Cnx_Upr2;
        Servo3.UpperJoint = Cnx_Upr3;
        Servo4.UpperJoint = Cnx_Upr4;
        Servo5.UpperJoint = Cnx_Upr5;
        Servo6.UpperJoint = Cnx_Upr6;
    }

    void Update()
    {
        Servo1.Crank_Length = crank_Length;
        Servo2.Crank_Length = crank_Length;
        Servo3.Crank_Length = crank_Length;
        Servo4.Crank_Length = crank_Length;
        Servo5.Crank_Length = crank_Length;
        Servo6.Crank_Length = crank_Length;

        if (FlipCranks == true)
        {
            Servo1.Handedness = ServoHandedness.Right;
            Servo2.Handedness = ServoHandedness.Left;
            Servo3.Handedness = ServoHandedness.Right;
            Servo4.Handedness = ServoHandedness.Left;
            Servo5.Handedness = ServoHandedness.Right;
            Servo6.Handedness = ServoHandedness.Left;
        }
        else
        {
            Servo1.Handedness = ServoHandedness.Left;
            Servo2.Handedness = ServoHandedness.Right;
            Servo3.Handedness = ServoHandedness.Left;
            Servo4.Handedness = ServoHandedness.Right;
            Servo5.Handedness = ServoHandedness.Left;
            Servo6.Handedness = ServoHandedness.Right;
        }

        Set_Azimuth(azimuth);
        Set_RodLength(rod_Length);
    }
    //----------------------------------------------------

    public void OnAzimuthInputChanged(string value)
    {
        float f = Convert.ToSingle(value, GlobalVars.myNumberFormat());
        azimuth = f;
    }
    public void OnCrankLengthInputChanged(string value)
    {
        float f = Convert.ToSingle(value, GlobalVars.myNumberFormat());
        crank_Length = f;
    }
    public void OnRodLengthInputChanged(string value)
    {
        float f = Convert.ToSingle(value, GlobalVars.myNumberFormat());
        rod_Length = f;
    }
    public void OnFlipCrankInputChanged(bool b)
    {
        FlipCranks = b;
    }

    void Set_Azimuth(float f)
    {
        //Base directions:
        float Line1 = 240;
        float Line2 = 0;
        float Line3 = 120;

        //Derviation from base direction:
        float deviation = f - 240;

        //calculate individual azimuths
        Servo1.Azimuth = Line1 + deviation;
        Servo2.Azimuth = Line1 - deviation;
        Servo3.Azimuth = Line2 + deviation;
        Servo4.Azimuth = Line2 - deviation;
        Servo5.Azimuth = Line3 + deviation;
        Servo6.Azimuth = Line3 - deviation;
    }
    void Set_RodLength(float f)
    {
        Servo1.Rod_Length = f;
        Servo2.Rod_Length = f;
        Servo3.Rod_Length = f;
        Servo4.Rod_Length = f;
        Servo5.Rod_Length = f;
        Servo6.Rod_Length = f;
    }

}
