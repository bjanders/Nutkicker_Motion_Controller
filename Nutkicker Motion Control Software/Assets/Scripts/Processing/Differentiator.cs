using UnityEngine;

[ExecuteInEditMode]
public class Differentiator : MonoBehaviour
{
    [SerializeField] private Stream InStream;
    [SerializeField] private Stream OutStream;
    [Space]
    [SerializeField] private float previous;
    [SerializeField] private float latest;
    [SerializeField] private float derivative;

    void Start()
    {
        OutStream = GetComponent<Stream>();

        if (InStream == null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();
            OutStream.name = InStream.name + "_Diff";
        }
    }
    void FixedUpdate()
    {
        CreateDerivative();
        
        OutStream?.Push(new Datapoint(Time.fixedTime, derivative, "derivative", "unknown"));
        
        previous = latest;
    }

    private void CreateDerivative()
    {
        latest = InStream.Youngest.Datavalue;
        derivative = (latest - previous) / Time.fixedDeltaTime;
    }
}
