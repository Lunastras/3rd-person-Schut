using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLevelPickUp : MonoBehaviour {


    PlayerMisc player;
    public int expValue;
    bool canWork = true;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(collisionStarter());

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
            player.getExp(expValue);
            Destroy(transform.parent.gameObject);

        }
    }

    private IEnumerator collisionStarter()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider>().enabled = true;

    }
}
