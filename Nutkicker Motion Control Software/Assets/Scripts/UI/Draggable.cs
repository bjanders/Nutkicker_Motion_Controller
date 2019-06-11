using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IDragHandler
{
    RectTransform transform = null;

    // Use this for initialization
    void Start()
    {
        transform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        transform.SetAsLastSibling();
    }
}