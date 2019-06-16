using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BarGraph : MonoBehaviour
{
    RectTransform parentRectTransform;
    [SerializeField] RectTransform rectTransform;
    [Header("Source")]
    [SerializeField] Stream InStream;
    [SerializeField] float value;
    [Header("Layout")]
    [SerializeField] Orientation orientation;
    [SerializeField] float posX;
    [SerializeField] float posY;
    [SerializeField] float thickness;
    [SerializeField] float pixelsPerG;

    private void Start()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        

        //length = InStream.Youngest.Datavalue;

        switch (orientation)
        {
            case Orientation.Vertical:
                break;
            case Orientation.Horizontal:
                if (value > 0)
                {
                    rectTransform.localPosition = new Vector2(posX, posY);
                    rectTransform.sizeDelta = new Vector2(value * pixelsPerG, thickness);
                }
                else
                {
                    rectTransform.localPosition = new Vector2(posX, posY) + new Vector2(value * pixelsPerG, 0);
                    rectTransform.sizeDelta = new Vector2(-value * pixelsPerG, thickness);
                }
                
                break;
            default:
                break;
        }
    }



    enum Orientation
    {
        Vertical,
        Horizontal
    }
}
