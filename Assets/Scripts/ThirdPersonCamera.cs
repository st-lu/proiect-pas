using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target, player;
    // public Transform camTransform;
    
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
}
