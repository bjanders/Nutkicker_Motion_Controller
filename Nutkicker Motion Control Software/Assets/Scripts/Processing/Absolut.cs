using UnityEngine;
using System;

[ExecuteInEditMode]
public class Absolut : MonoBehaviour
{
    [SerializeField] private Stream InStream;
    [SerializeField] private Stream OutStream;

    void Start()
    {
        OutStream = GetComponent<Stream>();

        if (InStream == null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();
            OutStream.name = InStream.name + "_ABS";
        }
    }
    private void FixedUpdate()
    {
        Datapoint datapoint = InStream.Youngest;
        OutStream.Push(new Datapoint(   Time.fixedTime,
                                        Math.Abs(datapoint.Datavalue),
                                        datapoint.Type,
                                        datapoint.Unit
                                        ));
    }
}
