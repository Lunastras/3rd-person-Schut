using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBulletScript : MonoBehaviour {


    public int speed;
    public float flyTime;
    

    // Use this for initialization
    void Start () {

        Destroy(gameObject, flyTime);

    }
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.forward * Time.deltaTime * speed;


    }
}
