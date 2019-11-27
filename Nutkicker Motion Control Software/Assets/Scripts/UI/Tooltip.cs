using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject WindowWithText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        WindowWithText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        WindowWithText.SetActive(false);
    }
}
    