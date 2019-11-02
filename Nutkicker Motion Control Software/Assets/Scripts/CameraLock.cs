using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class CameraLock : MonoBehaviour
{
    [SerializeField] private Transform target;                  //the object to stay locked to.
    [SerializeField] private Vector3 Offset;                    //Let the camera look to a point that is offset from th target.
    [SerializeField] private Vector2 StartPosition_Mouse;       //position of the mouse on the screen when the drag started
    [SerializeField] private Vector3 StartPosition_Camera;      //position of the camera in world coordinates when the drag started
    [SerializeField] private Vector3 StartVectorToCam;          //a vector pointing from the target to the camera when the drag started
    [SerializeField] private Vector2 Movement_Pixels;           //Movement of the drag in pixels
    [SerializeField] private Vector2 Movement_Angles;           //Corresponding angle of the drag in degrees
    [SerializeField] private int PixelPer180Deg = 500;          

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartPosition_Mouse = Input.mousePosition;
            StartVectorToCam = transform.position - target.position;
            return;
        }
        if (Input.GetMouseButton(1)) // 0 for left and 1 for right click
        {
            Movement_Pixels = (Vector2)Input.mousePosition - StartPosition_Mouse;

            Movement_Angles.y = 180 *  (Movement_Pixels.x / PixelPer180Deg);        //Lateral movement (delta x) will generate a rotation around the vertical axis (y)
            Movement_Angles.x = 180 *  (Movement_Pixels.y / PixelPer180Deg);        //Vertical movement (delta y) will generate a rotation around the lateral axis (x)

            Vector3 NewCamVector = Quaternion.Euler(-Movement_Angles.x, Movement_Angles.y, 0) * StartVectorToCam;
            
            transform.position = target.position + NewCamVector;
            transform.LookAt(target.position + Offset);
        }
    }

    private void FixedUpdate()
    {

        //transform.LookAt(target);
    }


}
