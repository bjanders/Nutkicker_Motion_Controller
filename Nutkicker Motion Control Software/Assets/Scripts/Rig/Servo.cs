using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServoHandedness
{
    Right,
    Left
}

[ExecuteInEditMode]
public class Servo : MonoBehaviour
{
    [Header("Geometry")]
    [SerializeField] public Transform BottomJoint;
    [SerializeField] public Transform UpperJoint;
    [SerializeField] private Transform Axis;
    [SerializeField] private Transform Crank_Rod_Joint;
    [Space]
    [SerializeField] public ServoHandedness Handedness;
    [SerializeField] public float Azimuth;         //In degrees
    [SerializeField] public float Crank_Length;
    [SerializeField] private float CrankAngleInternal;     //In degrees
    [SerializeField] private float CrankAngleTransmitted;     //In degrees
    [SerializeField] public float Rod_Length;
    [Space]
    [SerializeField] private Vector3 Delta;
    [Space]
    [SerializeField] private bool OutOfRange;

    private float LastGoodAngle; // We store calculated angle to use it if the new calculation has no solution (we stay where we were)
    private double COS_Azimuth;
    private double SIN_Azimuth;

    void Start()
    {
        Transform transform = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.SetPositionAndRotation(BottomJoint.position, Quaternion.identity);
        transform.localEulerAngles = new Vector3(0, Azimuth, 0);

        Crank_Rod_Joint.localPosition = new Vector3(Crank_Length * 20, 0, 0);

        CrankAngleInternal = calculateAngle();
        Axis.localEulerAngles = new Vector3(0, 0, CrankAngleInternal);

        CrankAngleTransmitted = calculateAngleTramsmitted();
    }

    private float calculateAngle()
    {
        Delta.x = UpperJoint.position.x - BottomJoint.position.x;
        Delta.y = UpperJoint.position.y - BottomJoint.position.y;
        Delta.z = UpperJoint.position.z - BottomJoint.position.z;

        //Optimisation:
        COS_Azimuth = Mathf.Cos(Utility.RAD_from_DEG(-Azimuth));
        SIN_Azimuth = Mathf.Sin(Utility.RAD_from_DEG(-Azimuth));

        double Term1 = 2.0f * Crank_Length * Delta.x * COS_Azimuth;
        double Term2 = 2.0f * Crank_Length * Delta.z * SIN_Azimuth;
        double Term3 = Crank_Length * Crank_Length - Rod_Length * Rod_Length + Delta.x * Delta.x + Delta.y * Delta.y + Delta.z * Delta.z;
        double Term4 = Term1 + Term2 + Term3;
        double RootTerm = Math.Sqrt(16.0 * (double)Crank_Length * (double)Crank_Length * (double)Delta.y * (double)Delta.y - 4.0 * (-Term1 - Term2 + Term3) * (Term4));

        Double Temp_angle;
        if (Handedness == ServoHandedness.Right)
        {
            Temp_angle = 2.0 * (Math.Atan((2.0 * (double)Crank_Length * (double)Delta.y - 0.5 * RootTerm) / (Term4)));         //angle in RADIANS
        }
        else
        {
            Temp_angle = 2.0 * (Math.Atan((2.0 * (double)Crank_Length * (double)Delta.y + 0.5 * RootTerm) / (Term4)));         //angle in RADIANS
        }

        CrankAngleInternal = Utility.DEG_from_RAD((float)Temp_angle);                                                        //angle in DEGREES

        //Do we have a valid result?
        if (!float.IsNaN(CrankAngleInternal))
        {
            OutOfRange = false;

            double Crank_x = Crank_Length * Mathf.Cos(Utility.RAD_from_DEG(CrankAngleInternal)) * COS_Azimuth;
            double Crank_y = Crank_Length * Mathf.Sin(Utility.RAD_from_DEG(CrankAngleInternal));
            double Crank_z = Crank_Length * Mathf.Cos(Utility.RAD_from_DEG(CrankAngleInternal)) * SIN_Azimuth;

            Vector3 CrankVector = new Vector3((float)Crank_x, (float)Crank_y, (float)Crank_z);

            LastGoodAngle = CrankAngleInternal;      //remember for next time
        }
        else
        {
            OutOfRange = true;
            CrankAngleInternal = LastGoodAngle;
        }

        return CrankAngleInternal;     //in Degrees
    }

    private float calculateAngleTramsmitted()
    {
        float tempangle = CrankAngleInternal;

        if (Handedness == ServoHandedness.Right)
        {
            return CrankAngleInternal;
        }
        else
        {
            if (CrankAngleInternal < 0)
            {
                tempangle += 360;
            }

            tempangle -= 180;
            tempangle *= -1;
        }

        return tempangle;
    }
}
