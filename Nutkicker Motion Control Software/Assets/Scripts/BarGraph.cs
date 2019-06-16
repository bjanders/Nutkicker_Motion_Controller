using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class BarGraph : MonoBehaviour
{
    RectTransform parentRectTransform;
    RectTransform rectTransform;
    [Header("Source")]
    [SerializeField] Stream InStream;
    [Header("Layout")]
    [SerializeField] Orientation orientation;
    [SerializeField] float posX;
    [SerializeField] float posY;
    [SerializeField][ShowOnly] float length;
    [SerializeField] float thickness;

    private void Start()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        //width = ParentRectTransform.rect.width;
        //height = ParentRectTransform.rect.height;
        //posX = ParentRectTransform.rect.x;
        //posY = ParentRectTransform.rect.y;
    }

    private void Update()
    {
        float value = 100.0f; //InStream.Youngest.Datavalue;

        switch (orientation)
        {
            case Orientation.Vertical:
                break;
            case Orientation.Horizontal:
                rectTransform.rect.Set(posX, posY, value, thickness);
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
