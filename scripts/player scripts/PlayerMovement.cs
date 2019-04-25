using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public float gravityScale;
    public bool isTalking = false;
    public bool canMove = true;


    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float jumpForce;

    private Vector3 moveDir;

    public float turnSmoothtime = 0.2f;
    float turnSmoothVelocity;


    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    public bool keyWasRaised = false;


    Transform cameraT;

    // Use this for initialization
    void Start () {

        controller = GetComponent<CharacterController>();
        cameraT = Camera.main.transform;

        }

// Update is called once per frame
void LateUpdate () {

        moveDir.y += Physics.gravity.y * gravityScale;
        controller.Move(moveDir * Time.deltaTime);

        moveDir = new Vector3(0, moveDir.y, 0);

        if (!canMove)
        {
            return;
        }

        float yStore = moveDir.y;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero || Input.GetButton("Fire1"))
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothtime);
        }


        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        //  moveDir.y = 0;


       
        moveDir = transform.forward * currentSpeed;
        moveDir.y = yStore;


        if (controller.isGrounded)
        {

            moveDir.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce;
            }
        }




    }
}
