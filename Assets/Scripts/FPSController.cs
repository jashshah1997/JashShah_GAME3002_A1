using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FPSController: MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public Text curr_goals;
    public Text max_goals;
    private bool start_shooting = false;
    private float start_shoot_time;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    
    void Start()
    {
        SoccerBallManager.Instance().Init(ParameterManager.MAXIMUM_BULLETS);

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        curr_goals.text = "" + SoccerBallManager.Instance().GetGoalCount();
        max_goals.text = "" + SoccerBallManager.Instance().GetMaximumGoals();

        Move();
        Fire();
    }

    private void Move()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;
        transform.position += moveDirection * Time.deltaTime;

        if (transform.position.z > -5.5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5.5f);
        }

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void Fire()
    {
        Vector3 offset = new Vector3(0, 0, 1.5f);
        offset = transform.rotation * offset;

        if (!start_shooting && Input.GetMouseButtonDown(0))
        {
            start_shoot_time = Time.time;
            start_shooting = true;
        }

        if (Input.GetMouseButtonUp(0) && SoccerBallManager.Instance().HasRemaningSoccerBalls())
        {
            var delta = Time.time - start_shoot_time;
            if (delta > 1)
            {
                delta = 1;
            }
            
            SoccerBallManager.Instance().GetBullet(
                transform.position + offset,
                Quaternion.Euler(0, (transform.rotation.eulerAngles.y - 90), -rotationX + 25),
                ParameterManager.BULLET_INITIAL_VELOCITY * (1 + delta));
            start_shooting = false;
        }
        
    }        
}