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
    Vector2 Movement;

    void Awake()
    {
        transform = GetComponent<RectTransform>();
    }
    public void OnMouseDown()
    {
        transform.SetAsLastSibling();
        OriginalWindowPosition = transform.position;        //remember well!
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStartPosition = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isOnScreen(eventData))
        {
            Movement = eventData.position - DragStartPosition;              //The movement of the MOUSE....
            transform.position = OriginalWindowPosition + Movement;         //...is being added to the original window position
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