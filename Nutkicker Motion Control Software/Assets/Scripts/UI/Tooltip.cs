using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject WindowWithText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
        WindowWithText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited");
        WindowWithText.SetActive(false);
    }
}
    