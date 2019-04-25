using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunRound : MonoBehaviour {

    public GameObject bullet;

	// Use this for initialization
	void Start () {


    }

    public void fire(int bulletsToInstantiate, float spread)
    {
        for (int i = 0; i < bulletsToInstantiate; i++)
        {
            GameObject instance = Instantiate(bullet, transform.position, transform.rotation);
            Vector3 offset = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            instance.transform.forward = Camera.main.transform.forward + offset;
            
        }

    }

    // Update is called once per frame
    void Update () {

        
		
	}
}
