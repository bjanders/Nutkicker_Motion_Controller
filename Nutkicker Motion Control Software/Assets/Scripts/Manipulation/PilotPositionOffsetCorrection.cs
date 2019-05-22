using System;
using UnityEngine;


public class PilotPositionOffsetCorrection : MonoBehaviour
{
    [Header("In-Streams")]
    [SerializeField] private Stream Ax;
    [SerializeField] private Stream Ay;
    [SerializeField] private Stream Az;
    [Header("Tool Streams")]
    [SerializeField] public Stream omega_x;
    [SerializeField] public Stream omega_y;
    [SerializeField] public Stream omega_z;
    [Space]
    [SerializeField] public Stream omega_dot_x;
    [SerializeField] public Stream omega_dot_y;
    [SerializeField] public Stream omega_dot_z;
    [Header("Offsets")]
    [SerializeField] public float delta_x;
    [SerializeField] public float delta_y;
    [SerializeField] public float delta_z;
    [Header("CorretionValues")]
    [SerializeField] private float cent_x;
    [SerializeField] private float cent_y;
    [SerializeField] private float cent_z;
    [Space]
    [SerializeField] private float tang_x;
    [SerializeField] private float tang_y;
    [SerializeField] private float tang_z;
    [Header("Corrected Values")]
    [SerializeField] private float corr_x;
    [SerializeField] private float corr_y;
    [SerializeField] private float corr_z;
    [Header("Out-Streams")]
    [SerializeField] public Stream Ax_corr;
    [SerializeField] public Stream Ay_corr;
    [SerializeField] public Stream Az_corr;
    
    void FixedUpdate()
    {
        Correct_Ax();
        Correct_Ay();
        Correct_Az();
    }

    private void Correct_Ax()
    {
        //Centrifugal acceleration:
        cent_x = -delta_x * (
                                Mathf.Pow(omega_y.Youngest.Datavalue, 2) +
                                Mathf.Pow(omega_z.Youngest.Datavalue, 2)
                            )   / 9.81f;                                            //division by 9.81 to convert m/s^2 into G.
        //Tangential acceleration:
        tang_x =    (omega_dot_y.Youngest.Datavalue * delta_z -
                    omega_dot_z.Youngest.Datavalue * delta_y)
                    /9.81f;                                                         //division by 9.81 to convert m/s^2 into G.

        corr_x =    (Ax.Youngest.Datavalue + cent_x + tang_x);

        Ax_corr.Push(new Datapoint(Time.fixedTime, corr_x, "Ax_corr", "G"));
    }
    private void Correct_Ay()
    {
        //Centrifugal acceleration:
        cent_y = -delta_y * (
                                Mathf.Pow(omega_x.Youngest.Datavalue, 2) +
                                Mathf.Pow(omega_z.Youngest.Datavalue, 2)
                            ) / 9.81f;                                            //division by 9.81 to convert m/s^2 into G.
        //Tangential acceleration:
        tang_y =    (-omega_dot_x.Youngest.Datavalue * delta_z +
                    omega_dot_z.Youngest.Datavalue * delta_x)
                    / 9.81f;                                                         //division by 9.81 to convert m/s^2 into G.

        corr_y = (Ay.Youngest.Datavalue + cent_y + tang_y);

        Ay_corr.Push(new Datapoint(Time.fixedTime, corr_y, "Ay_corr", "G"));
    }
    private void Correct_Az()
    {
        //Centrifugal acceleration:
        cent_z = -delta_z * (
                                Mathf.Pow(omega_x.Youngest.Datavalue, 2) +
                                Mathf.Pow(omega_y.Youngest.Datavalue, 2)
                            ) / 9.81f;                                            //division by 9.81 to convert m/s^2 into G.
        //Tangential acceleration:
        tang_z =    (omega_dot_x.Youngest.Datavalue * delta_y -
                    omega_dot_y.Youngest.Datavalue * delta_x)
                    / 9.81f;                                                        //division by 9.81 to convert m/s^2 into G.

        corr_z = (Az.Youngest.Datavalue + cent_z + tang_z);

        Az_corr.Push(new Datapoint(Time.fixedTime, corr_z, "Az_corr", "G"));
    }
}
