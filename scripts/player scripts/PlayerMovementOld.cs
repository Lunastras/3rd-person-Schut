using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOld : MonoBehaviour {

    public bool isTalking = false;

    public float moveSpeed;
    // public Rigidbody theRB;
    public CharacterController controller;
    public float jumpForce;
    private Vector3 moveDir;
    public float gravityScale;
    public bool canMove;
   // Transform cameraT;

	// Use this for initialization
	void Start () {

        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        //cameraT = Camera.main.transform;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //gravity 
        moveDir.y += Physics.gravity.y * gravityScale;
        controller.Move(moveDir * Time.deltaTime);

        if (!canMove)
        {
            moveDir = new Vector3(0, moveDir.y, 0);
            return;
        }



        float yStore = moveDir.y;
        moveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDir = moveDir.normalized * moveSpeed;
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
