using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5;
    public float smoothRotationTime = 0.1f;
    private float smoothRotationVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void DoUpdate()
    {
        // float moveX = InputHandler.Instance.GetHorizontalMovementInput();
        // float moveY = InputHandler.Instance.GetVerticalCameraInput();
        Vector3 moveDir = new Vector3(InputHandler.Instance.MoveInput.x, 0, InputHandler.Instance.MoveInput.y).normalized;
        if(moveDir.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRotationVelocity,
                smoothRotationTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            rb.AddForce(moveDir * (speed * Time.deltaTime));
        }
    }

    public void DoFixedUpdate()
    {
        
    }
}