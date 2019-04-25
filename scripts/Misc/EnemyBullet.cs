using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    bool hadInitialCollision = false; //when fired, it will collide with the enemy who fired it, so we gotta make sure it doesn't destroy
    public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if(hadInitialCollision && !other.gameObject.tag.Equals("Item"))
        {
            if (other.gameObject.name.Equals("Player"))
            {
                other.GetComponent<PlayerMisc>().damageGet(damage);
            }

            if(other.gameObject.tag.Equals("Enemy"))
            {
                other.GetComponent<EnemyHp>().damageGetEnemy(damage/2);
            }

            Destroy(transform.parent.gameObject);

        }
        else
        {
            hadInitialCollision = true;
        }
        
    }
}
