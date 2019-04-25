using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 offset;
    public Transform target;
    public float rotateSpeed;
    public bool useOffsetValues;
    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle;
    public Transform player;
    public int playerRotationSpeed;

    float lHorizontal;

	// Use this for initialization
	void Start () {

        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        Cursor.lockState = CursorLockMode.Locked;
        pivot.transform.parent = target.transform; 
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
         
        //move the player

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
      
        
      // player.rotation = Quaternion.LerpUnclamped(player.transform.rotation, Quaternion.Euler(0, transform.rotation.y, 0), Time.deltaTime * playerRotationSpeed);
         player.Rotate(0, horizontal, 0);

        //rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        //limit the up/down rotation
        if( pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f )
        {
            pivot.rotation = Quaternion.Euler( maxViewAngle, 0, 0 );
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < (360f - minViewAngle))
        {
            pivot.rotation = Quaternion.Euler( (360f - minViewAngle), 0, 0 );
        }

        //move the camera 

        float Yangle = target.eulerAngles.y;
        float Xangle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(Xangle, Yangle, 0);

        transform.position = target.position - (rotation * offset);



       // transform.position = target.position - offset;

        

        transform.LookAt(target.transform);

	}
}
