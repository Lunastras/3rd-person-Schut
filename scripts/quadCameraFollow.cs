using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadCameraFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(Camera.main.transform);
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(0, rot.y + 180, 0);
        transform.rotation = Quaternion.Euler(rot);

    }
}
