using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LerpToPosition : MonoBehaviour
{
    [SerializeField] Transform StartTransform;
    [SerializeField] Transform EndTransform;
    [SerializeField] [Range(0.0f, 1.0f)] float Percentage;

    void Update()
    {
        transform.position = Vector3.Lerp(StartTransform.position, EndTransform.position, Percentage);
        transform.rotation = Quaternion.Lerp(StartTransform.rotation, EndTransform.rotation, Percentage);
    }
}
