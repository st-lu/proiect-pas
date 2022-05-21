using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform target,player;
    // public Transform camTransform;
    public float distance = 5.0f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    // private float sensitivityX = 20.0f;
    // private float sensitivityY = 20.0f;

    private void Start()
    {
        // camTransform = transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        CamControl();
    }

    void CamControl(){
        currentX += Input.GetAxis("Mouse X") * 1;
        currentY -= Input.GetAxis("Mouse Y") * 1;
        currentY = Mathf.Clamp(currentY, -55, 25);

        transform.LookAt(target);

        if(Input.GetKey(KeyCode.LeftShift)){
            target.rotation = Quaternion.Euler(0, currentX, 0);
        }else{ 
            target.rotation = Quaternion.Euler(-currentY, currentX, 0);
            player.rotation = Quaternion.Euler(0, currentX+180, 0);
        }
         
    }
}
