using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IDragHandler
{
    RectTransform transform = null;

    // Use this for initialization
    void Awake()
    {
        transform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnMouseDown()
    {
        transform.SetAsLastSibling();
    }
}