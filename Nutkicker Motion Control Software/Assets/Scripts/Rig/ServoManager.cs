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
    [Range(0,0.5f)]
    [SerializeField] float crank_Length;
    [SerializeField] float Azimuth;         //The Azimuth of Servo Nr.1
    [SerializeField] bool FlipDirections;

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

        if (FlipDirections == true)
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

        Set_Azimuth();
    }

    void Set_Azimuth()
    {
        //Base directions:
        float Line1 = 240;
        float Line2 = 0;
        float Line3 = 120;

        //Derviation from base direction:
        float deviation = Azimuth - 240;

        //calculate individual azimuths
        Servo1.Azimuth = Line1 + deviation;
        Servo2.Azimuth = Line1 - deviation;
        Servo3.Azimuth = Line2 + deviation;
        Servo4.Azimuth = Line2 - deviation;
        Servo5.Azimuth = Line3 + deviation;
        Servo6.Azimuth = Line3 - deviation;
    }

}
