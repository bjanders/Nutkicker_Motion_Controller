using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visibility : MonoBehaviour
{
    [SerializeField] private bool visible = true;

    private void Start()
    {
        gameObject.SetActive(visible);
    }

    public void OnButtonClick()
    {
        if (visible)
        {
            visible = false;
            gameObject.SetActive(false);
        }
        else
        {
            visible = true;
            gameObject.SetActive(true);
        }
    }
}
