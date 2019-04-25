using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {



    public AudioClip damageNoise;
    public int damage;
    public float angleOffSet;
    // Use this for initialization
    void Start ()
    {

      transform.forward = Camera.main.transform.forward;
      //transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z);


    }



    // Update is called once per frame
   /* void Update ()
    {

       transform.position += transform.forward * Time.deltaTime * speed;
       transform.Translate(transform.forward * Time.deltaTime * speed);
    }*/

    

    private void OnTriggerEnter(Collider other)
    {
        
           

            if (other.gameObject.tag.Equals("Enemy"))
            {
                other.GetComponent<EnemyHp>().damageGetPlayer(damage);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(damageNoise);


            }

       

            if(!other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("Item") && !other.gameObject.tag.Equals("Detection Box"))
            {
            Debug.Log(other.gameObject.tag);
            Destroy(gameObject);
            }



    }



}
