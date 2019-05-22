using UnityEngine;

[ExecuteInEditMode]
public class CubeIndicator : MonoBehaviour
{
    [SerializeField] private Stream stream;
    [SerializeField] private float rawValue;
    [SerializeField] private float gain = 1;
    [SerializeField] private float scaledValue = 1;

    void Update()
    {
        rawValue = stream.Youngest.Datavalue;
        scaledValue = rawValue * gain;
        

        float x = transform.localPosition.x;
        float y = scaledValue;
        float z = transform.localPosition.z;
        
        transform.localPosition = new Vector3(x, y, z); 
    }
}
