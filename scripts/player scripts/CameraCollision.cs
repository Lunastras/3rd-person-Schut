using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

    public float minDistance = 3.0f;
    public float maxDistance = 4.0f;
    public float smooth = 1.0f;
    public float cameraSpeed = 30f;

    public Vector3 dollyDirAdjusted;
    public float distance;

	// Use this for initialization
	void Awake () {

        distance = transform.position.magnitude;

		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        Vector3 desiredCameraPosition = GetComponent<ThirdPersonCamera>().moveDir;
        RaycastHit hit;


       

       if(Physics.Linecast(transform.position, desiredCameraPosition, out hit))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        GetComponent<ThirdPersonCamera>().distanceFromTarget = Mathf.SmoothDamp(GetComponent<ThirdPersonCamera>().distanceFromTarget, distance, ref cameraSpeed, smooth);

         //transform.position = Vector3.Lerp(transform.position, moveDir, Time.deltaTime * rotationSmoothTime);


        // transform.position = Vector3.Lerp(transform.position, dollyDir * distance, Time.deltaTime * smooth);

    }
}
