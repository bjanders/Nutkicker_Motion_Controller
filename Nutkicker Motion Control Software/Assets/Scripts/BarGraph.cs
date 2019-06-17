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
    [SerializeField] bool Active;
    [SerializeField] bool Invert;
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
        if (Active)
        {
            value = InStream.Youngest.Datavalue;

            if (Invert)
            {
                value *= -1;
            }
        }

        

        switch (orientation)
        {
            case Orientation.Vertical:
                if (value > 0)
                {
                    rectTransform.localPosition = new Vector2(posX, posY) + new Vector2(0, value * pixelsPerG);
                    rectTransform.sizeDelta = new Vector2(thickness, value * pixelsPerG);
                }
                else
                {
                    rectTransform.localPosition = new Vector2(posX, posY);
                    rectTransform.sizeDelta = new Vector2(thickness, -value * pixelsPerG);
                   
                }
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
