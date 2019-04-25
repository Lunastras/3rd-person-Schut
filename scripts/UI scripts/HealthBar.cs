using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

   

    public PlayerMisc player;
    public float hpDif;

    // Use this for initialization
    void Start () {

        player = FindObjectOfType<PlayerMisc>();
		
	}
	
	// Update is called once per frame
	void Update () {

        hpDif = (float)player.playerHP / player.hpMax;

        gameObject.GetComponent<Image>().fillAmount = hpDif;

       // Debug.Log( double(player.playerHP) / player.hpMax);

        


    }
}
