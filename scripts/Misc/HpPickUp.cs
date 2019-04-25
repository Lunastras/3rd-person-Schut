﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUp : MonoBehaviour {

    PlayerMisc player;
    public int hpValue;
    bool canWork = true;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerMisc>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Player") && canWork)
        {
            canWork = false;
            player.getHP(hpValue);
            Destroy(transform.parent.gameObject);
           
        }
    }
}
