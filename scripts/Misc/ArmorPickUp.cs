using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickUp : MonoBehaviour {

    PlayerMisc player;
    public int armorValue;
    bool canWork = true;

    // Use this for initialization
    void Start()
    {

        player = FindObjectOfType<PlayerMisc>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player") && canWork)
        {
            canWork = false;
            player.getArmor(armorValue);
            Destroy(transform.parent.gameObject);

        }
    }
}
