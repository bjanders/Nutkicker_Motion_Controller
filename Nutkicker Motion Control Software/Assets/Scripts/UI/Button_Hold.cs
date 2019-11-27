using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Aft
}

public class Button_Hold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool PointerDown;
    [SerializeField] private Platform platform;
    [SerializeField] private PanelRigConfig panelRigConfig;
    [Space]
    [SerializeField] private Direction direction;
    //speeds and accelerations:
    [SerializeField] private float Speed_current;           //in m/s
    [SerializeField] private float Acceleration = 0.1f;     //in m/s^2
    [SerializeField] private float Deceleration;            //in m/s^2
    [SerializeField] private float BrakeTime = 1.0f;        //in seconds to come to a full stop


    private void FixedUpdate()
    {
        if(PointerDown)
        {
            Speed_current += Acceleration * Time.fixedDeltaTime;
        }

        if (Speed_current > 0)
        {
            switch (direction)
            {
                case Direction.Up:
                    platform.transform.Translate(new Vector3(0, Speed_current, 0));
                    panelRigConfig.UpdateInputs();
                    break;

                case Direction.Down:
                    platform.transform.Translate(new Vector3(0, -Speed_current, 0));
                    panelRigConfig.UpdateInputs();
                    break;

                case Direction.Right:
                    platform.transform.Translate(new Vector3(Speed_current, 0, 0));
                    panelRigConfig.UpdateInputs();
                    break;

                case Direction.Left:
                    platform.transform.Translate(new Vector3(-Speed_current, 0, 0));
                    panelRigConfig.UpdateInputs();
                    break;

                case Direction.Forward:
                    platform.transform.Translate(new Vector3(0, 0, Speed_current));
                    panelRigConfig.UpdateInputs();
                    break;

                case Direction.Aft:
                    platform.transform.Translate(new Vector3(0, 0, -Speed_current));
                    panelRigConfig.UpdateInputs();
                    break;

                default:
                    Debug.LogError("Why the hell did nothing happen here???");
                    break;
            }

            
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown = true;
        StopCoroutine(ReduceSpeed());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PointerDown = false;
        StartCoroutine(ReduceSpeed());
    }

    //Coroutines:
    IEnumerator ReduceSpeed()
    {
        if (BrakeTime <= 0.0f)
        {
            BrakeTime = 0.01f;      //to prevent division by zero or negative values
        }

        Deceleration = Speed_current / BrakeTime;

        //make sure the speed drops to zero within X seconds
        while (Speed_current > 0)
        {
            Speed_current -= Deceleration * Time.deltaTime;
            yield return null;
        }
        Speed_current = 0;
    }
}
