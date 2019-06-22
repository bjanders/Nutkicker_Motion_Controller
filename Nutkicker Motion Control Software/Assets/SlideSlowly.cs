using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SliderStatus
{
    Park,
    GoingUp,
    GoingDown,
    Active
}
public class SlideSlowly : MonoBehaviour
{
    [SerializeField] private float TransitTime = 5.0f;
    [SerializeField] private Slider slider;
    [SerializeField] private SliderStatus status;
    IEnumerator myCoroutine;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void On_Click()
    {
        switch (status)
        {
            case SliderStatus.Park:
                status = SliderStatus.GoingUp;
                myCoroutine = Slide(1);
                StartCoroutine(myCoroutine);
                break;

            case SliderStatus.GoingUp:
                status = SliderStatus.GoingDown;
                myCoroutine = Slide(0);
                StartCoroutine(myCoroutine);
                break;

            case SliderStatus.GoingDown:
                status = SliderStatus.GoingUp;
                myCoroutine = Slide(1);
                StartCoroutine(myCoroutine);
                break;

            case SliderStatus.Active:
                status = SliderStatus.GoingDown;
                myCoroutine = Slide(0);
                StartCoroutine(myCoroutine);
                break;

            default:
                break;
        }
    }

    IEnumerator Slide(int target)
    {




        yield return null;
    }
}
