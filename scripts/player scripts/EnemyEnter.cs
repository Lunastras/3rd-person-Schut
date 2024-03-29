﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnter : MonoBehaviour {

	/*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Equals("DetectionRadius"))
        {
            
            other.transform.GetComponentInParent<EnemyGroundBehaviour>().gotInCombat(gameObject.transform.parent.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("ChaseRadius"))
        {
            other.transform.GetComponentInParent<EnemyGroundBehaviour>().outOfCombat();
        }

    }
}
