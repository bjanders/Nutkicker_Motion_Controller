//Alternative:

//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using System.Collections;

//public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    //[SerializeField] private Transform target;
//    [SerializeField] private bool shouldReturn;


//    private bool isMouseDown = false;
//    private Vector3 startMousePosition;
//    private Vector3 startPosition;

//    private void Start()
//    {
//        Transform transform = GetComponent<Transform>();
//    }

//    public void OnPointerDown(PointerEventData dt)
//    {
//        isMouseDown = true;

//        startPosition = transform.position;
//        startMousePosition = Input.mousePosition;
//    }

//    public void OnPointerUp(PointerEventData dt)
//    {
//        isMouseDown = false;

//        if (shouldReturn)
//        {
//            transform.position = startPosition;
//        }
//    }

//    void Update()
//    {
//        if (isMouseDown)
//        {
//            Vector3 currentPosition = Input.mousePosition;
//            Vector3 diff = currentPosition - startMousePosition;
//            Vector3 pos = startPosition + diff;

//            transform.position = pos;
//        }
//    }
//}


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IDragHandler
{
    RectTransform m_transform = null;

    // Use this for initialization
    void Start()
    {
        m_transform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_transform.position += new Vector3(eventData.delta.x, eventData.delta.y);

        // magic : add zone clamping if's here.
    }
}