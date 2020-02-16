using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.Events;
using TMPro;

[ExecuteInEditMode]
public class Platform : MonoBehaviour
{
    #region References
    [Header("Geometry")]
    [Range(0, 120)]
    [SerializeField] public float Alpha;
    [Range(0, 120)]
    [SerializeField] private float Beta;
    [Range(0.1f, 2.0f)]
    [SerializeField] public float Radius;
    [Space]
    [SerializeField] Transform ConnectPoint1;
    [SerializeField] Transform ConnectPoint2;
    [SerializeField] Transform ConnectPoint3;
    [SerializeField] Transform ConnectPoint4;
    [SerializeField] Transform ConnectPoint5;
    [SerializeField] Transform ConnectPoint6;
    [Space]
    [SerializeField] private float A1;
    [SerializeField] private float A2;
    [SerializeField] private float A3;
    [SerializeField] private float A4;
    [SerializeField] private float A5;
    [SerializeField] private float A6; 
    #endregion

    void Update()
    {
        CalculateAllAngles();
        DrawAllConnectors();
    }

    void CalculateAllAngles()
    {
        Beta = 120 - Alpha;

        A1 = Alpha/2;
        A2 = A1 + Beta;
        A3 = A2 + Alpha;
        A4 = A3 + Beta;
        A5 = A4 + Alpha;
        A6 = A5 + Beta;
    }
    private void DrawAllConnectors()
    {
        DrawSingleConnectorAtAngle(ConnectPoint1, A1);
        DrawSingleConnectorAtAngle(ConnectPoint2, A2);
        DrawSingleConnectorAtAngle(ConnectPoint3, A3);
        DrawSingleConnectorAtAngle(ConnectPoint4, A4);
        DrawSingleConnectorAtAngle(ConnectPoint5, A5);
        DrawSingleConnectorAtAngle(ConnectPoint6, A6);
    }
    void DrawSingleConnectorAtAngle(Transform TF, float angle_deg)
    {
        //float radius = GetComponent<QcTorusMesh>().properties.radius;
        //float angle_rad = (angle_deg / 360) * 2 * Mathf.PI;
        float angle_rad = Utility.RAD_from_DEG(angle_deg);

        float x = Mathf.Sin(angle_rad) * Radius;
        float y = 0;
        float z = Mathf.Cos(angle_rad) * Radius;

        if (TF != null)
        {
            TF.localPosition = new Vector3(x,y,z);
        }
        
    }

    public void OnAlphaChanged(string value)
    {
        Alpha = Convert.ToSingle(value, GlobalVars.myNumberFormat());
    }
    public void OnRadiusChanged(string value)
    {
        Radius = Convert.ToSingle(value, GlobalVars.myNumberFormat());
    }
    public void OnPosition_X_Changed(string value)
    {
        float temp = Convert.ToSingle(value,GlobalVars.myNumberFormat());
        transform.localPosition = new Vector3(  temp,
                                                transform.localPosition.y,
                                                transform.localPosition.z   );
    }
    public void OnPosition_Y_Changed(string value)
    {
        float temp = Convert.ToSingle(value, GlobalVars.myNumberFormat());
        transform.localPosition = new Vector3(  transform.localPosition.x,
                                                temp,
                                                transform.localPosition.z);
    }
    public void OnPosition_Z_Changed(string value)
    {
        float temp = Convert.ToSingle(value, GlobalVars.myNumberFormat());
        transform.localPosition = new Vector3(  transform.localPosition.x,
                                                transform.localPosition.y,
                                                temp);
    }

}
