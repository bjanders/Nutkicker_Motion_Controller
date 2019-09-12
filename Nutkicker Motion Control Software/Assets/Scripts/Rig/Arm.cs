using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Arm : MonoBehaviour
{
    [SerializeField] Transform Top;
    [SerializeField] Transform Bottom;
    [SerializeField] private float Thickness;
    [SerializeField] private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawLineBetweenTwoPoints(Bottom.position, Top.position, Thickness);
        lineRenderer.SetWidth(Thickness, Thickness);
    }

    void DrawLineBetweenTwoPoints(Vector3 Bottom, Vector3 Top, float thickness)
    {
        lineRenderer.SetPosition(0, Bottom);
        lineRenderer.SetPosition(1, Top);
    }
}
