using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    float yaw;
    float pitch;
    public Vector2 pitchMinMax = new Vector2(-70, 85);

    public float sensitivity = 10;

    public float maxDistance = 10f;
    public float minDistance = 1f;


    public Transform target;
    public float distanceFromTarget;
   // public float distanceNormal;
    public float distanceOnZoom;

    public float minFoV = 40;
    public float currentFoV=60;
    public float maxFoV = 60;
    float targetDist = 9;

    public float rotationSmoothTime = 1.2f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;



    public Vector3 offset;
    public float cameraZoomSpeed;
    public Transform player;
    public Vector3 moveDir;
    public bool canMove;

    bool IsNotColliding;
	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - player.position;
		
	}
	
	// Update is called once per frame
	void Update () {



        

        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        



        


        // currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = new Vector3(pitch, yaw);


        /*if(distanceFromTarget < targetDist && IsNotColliding)
        {
          
            RaycastHit hit;
            float auxDistance = distanceFromTarget + cameraZoomSpeed/2 * Time.deltaTime;

            Vector3 auxMoveDir = target.position - auxDistance * transform.forward;


            if (!Physics.Linecast(transform.position, auxMoveDir, out hit))
            {
               // if(hit.distance > 10.0f)
               // {
                    //Debug.Log(hit.distance);

                    moveDir = auxMoveDir;
                    distanceFromTarget = auxDistance;
              //  }
               
            }*/


        //}*/

        //moveDir = target.position - distanceFromTarget* transform.forward;

        // transform.position = Vector3.Lerp(transform.position, moveDir, Time.deltaTime * rotationSmoothTime);


        RaycastHit hit;

        //distanceFromTarget = maxDistance;





        moveDir = target.position - distanceFromTarget * transform.forward;



        transform.position = moveDir;

        bool hasObjectBetween = true;

        bool zooming = Input.GetAxis("Zoom") > 0;
       // targetDist = (zooming) ? distanceOnZoom : maxDistance;
        float targetFoV = (zooming) ? minFoV : maxFoV;

        if (Physics.Linecast(target.position, transform.position, out hit))
        {       
            Debug.Log("The raycast hit and the dist is" + hit.distance + " " + hit.collider.gameObject.name);

            if (hit.distance > 0)
            {
                targetDist = hit.distance;
            }
        }
        else
        {
            if(!Physics.Linecast(target.position, target.position + transform.forward * maxDistance, out hit))
            {
                targetDist = maxDistance;

            }
        }

       // targetDist = Mathf.Round(targetDist);




        distanceFromTarget = Mathf.SmoothDamp(distanceFromTarget, targetDist, ref speedSmoothVelocity, speedSmoothTime);
        Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, targetFoV, ref speedSmoothVelocity, speedSmoothTime);

        /*
        if(distanceFromTarget + 0.5f > targetDist)
        {
            distanceFromTarget = targetDist;
        }
        if (Camera.main.fieldOfView + 0.5f > targetFoV)
        {
            Camera.main.fieldOfView = targetFoV;
        }*/


        //clamp the values
        //distanceFromTarget = Mathf.Clamp(distanceFromTarget, minDistance+1, maxDistance-1);
        //Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFoV + 1, maxFoV - 1);


        if(distanceFromTarget > targetDist)
        {
            if(distanceFromTarget - 0.1f < targetDist)
            {
                distanceFromTarget = targetDist;
            }
        }
        else
        {
            if (distanceFromTarget + 0.1f > targetDist)
            {
                distanceFromTarget = targetDist;
            }
        }





    }






}
