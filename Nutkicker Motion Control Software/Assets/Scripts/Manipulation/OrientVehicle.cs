using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrientVehicle : MonoBehaviour
{
    [Header("Vehicle")]
    [SerializeField] private Transform transform;
    [Header("Streams")]
    [SerializeField] Stream HdgStream;
    [SerializeField] Stream PitchStream;
    [SerializeField] Stream BankStream;
    [Header("Euler Angles")]
    [SerializeField] private float Hdg;
    [SerializeField] private float Pitch;
    [SerializeField] private float Bank;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        Hdg = HdgStream.Youngest.Datavalue;
        Pitch = PitchStream.Youngest.Datavalue;
        Bank = BankStream.Youngest.Datavalue;

        Hdg *= Convert.ToSingle((360.0f / (2 * Math.PI)));
        Pitch *= Convert.ToSingle((-360.0f / (2 * Math.PI)));
        Bank *= Convert.ToSingle((-360.0f / (2 * Math.PI)));

        transform.eulerAngles = new Vector3(Pitch, Hdg, Bank);
    }
}
