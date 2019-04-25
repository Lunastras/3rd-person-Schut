using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{



    public AudioClip damageNoise;
    public int damage;
    public float angleOffSet;
    // Use this for initialization
    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponent<EnemyHp>().damageGetPlayer(damage);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(damageNoise);


        }



        if (!other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("Item") && !other.gameObject.tag.Equals("Detection Box"))
        {
            Debug.Log(other.gameObject.tag);
            Destroy(gameObject);
        }



    }



}
