using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Lerp2Target : MonoBehaviour
{
    [SerializeField] Transform StartTransform;
    [SerializeField] Transform EndTransform;
    [SerializeField] [Range(0.0f, 1.0f)] public float Percentage;
    [SerializeField] [Range(0.0f, 1.0f)] public float SmoothValue;

    void Update()
    {
        SmoothValue = 0.5f * (Mathf.Cos(Mathf.PI * (1 - Percentage)) + 1);

        transform.position = Vector3.Lerp(StartTransform.position, EndTransform.position, SmoothValue);
        transform.rotation = Quaternion.Lerp(StartTransform.rotation, EndTransform.rotation, SmoothValue);
    }
}