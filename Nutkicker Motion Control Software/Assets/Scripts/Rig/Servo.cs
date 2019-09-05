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
    [SerializeField] private Transform BottomJoint;
    [SerializeField] private Transform UpperJoint;
    [SerializeField] private Transform Axis;
    [SerializeField] private Transform Crank_Rod_Joint;
    [Space]
    [SerializeField] public ServoHandedness Handedness;
    [SerializeField] private float Azimuth;
    [SerializeField] public float Crank_Length;
    [SerializeField] private float Crank_Angle;
    [SerializeField] private float Rod_Length;
    [Space]
    [SerializeField] private Vector3 Delta;
    [Space]
    [SerializeField] private bool OutOfRange;

    private float LastGoodAngle; // We store calculated angle to use it if the new calculation has no solution (we stay where we where)
    private float COS_Azimuth;
    private float SIN_Azimuth;

    void Start()
    {
        Transform transform = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.SetPositionAndRotation(BottomJoint.position, Quaternion.Euler(0, Azimuth, 0));

        Vector3 Offset;

        if (Handedness == ServoHandedness.Right)
        {
            Crank_Rod_Joint.localPosition = new Vector3(Crank_Length * 20, 0, 0);
        }
        else
        {
            Crank_Rod_Joint.localPosition = new Vector3(-Crank_Length * 20, 0, 0);
        }

        Crank_Angle = calculateAngle();
        Axis.localEulerAngles = new Vector3(0, 0, Crank_Angle);
    }

    private float calculateAngle()
    {
        Delta.x = UpperJoint.position.x - BottomJoint.position.x;
        Delta.y = UpperJoint.position.y - BottomJoint.position.y;
        Delta.z = UpperJoint.position.z - BottomJoint.position.z;

        //Optimisation:
        COS_Azimuth = Mathf.Cos(Utility.RAD_from_DEG(Azimuth));
        SIN_Azimuth = Mathf.Sin(Utility.RAD_from_DEG(Azimuth));

        float Term1 = 2.0f * Crank_Length * Delta.x * COS_Azimuth;
        float Term2 = 2.0f * Crank_Length * Delta.z * SIN_Azimuth;
        float Term3 = Crank_Length * Crank_Length - Rod_Length * Rod_Length + Delta.x * Delta.x + Delta.y * Delta.y + Delta.z * Delta.z;
        float Term4 = Term1 + Term2 + Term3;
        float RootTerm = Mathf.Sqrt(16.0f * Crank_Length * Crank_Length * Delta.y * Delta.y - 4.0f * (-Term1 - Term2 + Term3) * (Term4));

        Crank_Angle = 2.0f * (Mathf.Atan((2.0f * Crank_Length * Delta.y - 0.5f * RootTerm) / (Term4)));

        //Do we have a valid result?
        if (!float.IsNaN(Crank_Angle))
        {
            OutOfRange = false;

            float Crank_x = Crank_Length * Mathf.Cos(Utility.RAD_from_DEG(Crank_Angle)) * COS_Azimuth;
            float Crank_y = Crank_Length * Mathf.Sin(Utility.RAD_from_DEG(Crank_Angle));
            float Crank_z = Crank_Length * Mathf.Cos(Utility.RAD_from_DEG(Crank_Angle)) * SIN_Azimuth;

            Vector3 CrankVector = new Vector3(Crank_x, Crank_y, Crank_z);

            LastGoodAngle = Utility.RAD_from_DEG(Crank_Angle);      //remember for next time
        }
        else
        {
            OutOfRange = true;
            Crank_Angle = LastGoodAngle;
        }

        return Utility.DEG_from_RAD( Crank_Angle);
    }
}
