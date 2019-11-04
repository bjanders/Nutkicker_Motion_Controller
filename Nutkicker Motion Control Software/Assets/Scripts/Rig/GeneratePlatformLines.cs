using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GeneratePlatformLines : MonoBehaviour
{
    [SerializeField] LineRenderer linerenderer;
    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;
    [SerializeField] Transform Point3;
    [SerializeField] Transform Point4;
    [SerializeField] Transform Point5;
    [SerializeField] Transform Point6;
    [SerializeField] Transform[] Points;
    [SerializeField] Vector3[] Positions;

    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
        Points = new Transform[] { Point1, Point2, Point3, Point4, Point5, Point6 };
        Positions = new Vector3[6];
    }

    void Update()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            Positions[i] = Points[i].position;
        }

        linerenderer.SetPositions(Positions);
    }
}
