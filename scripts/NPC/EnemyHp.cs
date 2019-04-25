using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour {

    EnemyGroundBehaviour comb;
    public int enemyHP;
    public AudioClip deathSound;
    public AudioClip damageNoise;
    AudioSource audio;
    public GameObject soundMaker;
    public GameObject droppedExp;
    public int expToSpawn;
    private bool hasDied = false;
    // Use this for initialization
    void Start ()
    {


        audio = GetComponent<AudioSource>();
        comb = GetComponent<EnemyGroundBehaviour>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void enemyDeath()
    {
        if(!hasDied)
        {
            hasDied = true;

            GameObject sound = Instantiate(soundMaker);
            sound.GetComponent<AudioSource>().PlayOneShot(deathSound);
            Destroy(sound, deathSound.length);

            for (int i = 0; i < expToSpawn; i++)
            {
                GameObject exp = Instantiate(droppedExp);
                exp.transform.rotation = Random.rotation;
                exp.transform.position = transform.position;
                exp.GetComponent<Rigidbody>().AddForce(exp.transform.forward * 2000f);

            }
            Destroy(gameObject);

        }
       

    }



    public void damageGetEnemy(int damage)
    {


        enemyHP -= damage;
        if(enemyHP <= 0)
        {
            enemyDeath();
        }
        else
        {

            StartCoroutine(comb.fireFlash(1, 0, Color.white));
            FloatingTextController.createFloatingText(damage.ToString(), transform, "EnemyDamageText");
        }


    }

    public void damageGetPlayer(int damage)
    {


        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            enemyDeath();
        }
        else
        {
            if (!comb.isChasing)
            {
                comb.gotInCombat(FindObjectOfType<PlayerMisc>().transform);
            }
            StartCoroutine(comb.fireFlash(1, 0, Color.white));
            FloatingTextController.createFloatingText(damage.ToString(), transform, "EnemyDamageText");
        }


    }
}
