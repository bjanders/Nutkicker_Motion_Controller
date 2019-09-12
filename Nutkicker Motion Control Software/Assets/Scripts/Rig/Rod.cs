using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rod : MonoBehaviour
{
    [SerializeField] Transform Beginning;
    [SerializeField] Transform End;
    [SerializeField] private float Thickness;
    [SerializeField] private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawLineBetweenTwoPoints(End.position, Beginning.position, Thickness);
        //lineRenderer.SetWidth(Thickness, Thickness);      //Obsolete!
        lineRenderer.startWidth = Thickness;
        lineRenderer.endWidth = Thickness;
        
    }

    void DrawLineBetweenTwoPoints(Vector3 Bottom, Vector3 Top, float thickness)
    {
        lineRenderer.SetPosition(0, Bottom);
        lineRenderer.SetPosition(1, Top);
    }
}