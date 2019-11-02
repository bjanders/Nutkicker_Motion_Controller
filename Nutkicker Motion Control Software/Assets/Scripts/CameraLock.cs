using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraLock : MonoBehaviour
{
    [SerializeField]
    private Transform target;            //the object to stay locked to.

    private void FixedUpdate()
    {
        transform.LookAt(target);
    }


}
