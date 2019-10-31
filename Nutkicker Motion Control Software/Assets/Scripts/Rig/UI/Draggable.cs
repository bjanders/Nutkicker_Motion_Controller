using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[ExecuteInEditMode]
public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    RectTransform transform = null;

    Vector2 OriginalWindowPosition;
    Vector2 DragStartPosition;
    Vector2 DistToAnchor;

    void Awake()
    {
        transform = GetComponent<RectTransform>();
    }
    public void OnMouseDown()
    {
        transform.SetAsLastSibling();                                       //to bring the window to the top of the stack 
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OriginalWindowPosition = transform.position;                        
        DistToAnchor = OriginalWindowPosition - eventData.position;         //the offset to the Anchor.
        DragStartPosition = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isOnScreen(eventData))
        {
            transform.position = eventData.position + DistToAnchor;
            return;
        }
    }
    bool isOnScreen (PointerEventData eventData)
    {
        if (eventData.position.x >= 0 && eventData.position.x <= Screen.width && eventData.position.y >= 0 && eventData.position.y <= Screen.height)
        {
            return true;
        }
        return false;
    }
}