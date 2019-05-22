using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Actuator : MonoBehaviour
{
    [SerializeField] Transform Bottom;
    [SerializeField] Transform Top;
    [SerializeField] private float Diameter;
    [Header("Dimensions")]
    [SerializeField] private float MinLength;
    [SerializeField] private float MaxLength;
    [SerializeField] private float Stroke;
    [SerializeField] public float CurrentLength;
    [SerializeField] public float Extension;
    [Header("Utilisation")]
    [SerializeField] public float Utilisation;
    [SerializeField] public bool TooLong;
    [SerializeField] public bool TooShort;
    [SerializeField] public bool InLimits;
    [Header("Colors")]
    [SerializeField] Material MaterialLong;
    [SerializeField] Material MaterialInLimits;
    [SerializeField] Material MaterialShort;

    Renderer rend;
    Actuators actuators;
    ////////////////////////////////////////////////////////////
    void Start()
    {
        rend = GetComponent<Renderer>();
        actuators = transform.parent.gameObject.GetComponent<Actuators>();
    }
    void Update()
    {
        Diameter =  actuators.Diameter;
        MinLength = actuators.MinLength;
        MaxLength = actuators.MaxLength;
        Stroke = (MaxLength - MinLength);
        Extension = CurrentLength - MinLength;
        
        if (Stroke > 0)
        {
            Utilisation = Extension / Stroke;
        }
        else
        {
            Debug.LogError("Actuator may not have a stroke of zero or less");
        }

        OrientCylinderBetweenPoints(Bottom.position, Top.position, Diameter);
        CheckLimits();
        ColorCylinder();
    }
    ////////////////////////////////////////////////////////////
   
    void OrientCylinderBetweenPoints(Vector3 Bottom, Vector3 Top, float Diameter)
    {
        
        Vector3 Direction = Top - Bottom;
        Vector3 Scale = new Vector3(Diameter, Direction.magnitude / 2.0f, Diameter);
        Vector3 Position = Bottom + Direction / 2.0f;

        Transform transform = GetComponent<Transform>();
        transform.position = Position;
        transform.up = Direction;
        transform.localScale = Scale;

        CurrentLength = Direction.magnitude;
    }
    void CheckLimits()
    {
        if (CurrentLength > MaxLength)
        {
            TooLong = true;
            TooShort = false;
        }
        else if (CurrentLength < MinLength)
        {
            TooLong = false;
            TooShort = true;
        }
        else
        {
            TooLong = false;
            TooShort = false;
        }

        InLimits = !TooLong && !TooShort;
    }
    void ColorCylinder()
    {
        if (TooLong)
        {
            rend.material = MaterialLong;
        }
        else if(TooShort)
        {
            rend.material = MaterialShort;
        }
        else
        {
            rend.material = MaterialInLimits;
        }
    }
}
