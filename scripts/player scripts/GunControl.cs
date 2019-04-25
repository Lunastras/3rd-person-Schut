using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GunControl : MonoBehaviour {

    public GameObject FiringPoint;
    public float angleOffSet;
    public bool canFire = true;
    public AudioClip gunFireSound;
    private bool fireKeyWasRaised = true;

    

    public bool isAutomatic;
    public float fireRate;


    public GameObject standardBullet; //current bullet
    public GameObject genericBullet;
    public GameObject shotgunRound;
    private bool usingShotgun;
    private int shotgunCount;
    private float shotgunSpread;
  
    AudioSource audio;

    

    // Use this for initialization
    void Start () {

        audio = GetComponent<AudioSource>();


        

    }
	
	// Update is called once per frame
	void Update () {

        /* if(Input.GetAxisRaw("WeaponChange") > 0)
         {

         }*/

        if (isAutomatic)
        {
            if (Input.GetButton("Fire1") && canFire)
            {
                fire();

            }
        }
        else 
        {
            if (Input.GetButton("Fire1"))
            {
                if ((canFire) && fireKeyWasRaised)
                {
                    fireKeyWasRaised = false;
                    fire();
                }

            }
            else
            {
                fireKeyWasRaised = true;
            }
        }
       
        
    

       

    }

    private IEnumerator fireCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

   
   

    private void fire()
    {
        audio.PlayOneShot(gunFireSound);
        StartCoroutine(fireCoolDown());
        if(usingShotgun)
        {
            GameObject instance = Instantiate(standardBullet, FiringPoint.transform.position, transform.rotation);
            instance.GetComponent<ShotgunRound>().fire(shotgunCount, shotgunSpread);
        }
        else
        {
            Instantiate(standardBullet, FiringPoint.transform.position, transform.rotation);
        }


    }


    public void setGun(int currentWeapon, int level)
    {
        usingShotgun = false;

        switch (currentWeapon)
        {
            case 0:
                standardBullet = genericBullet;
                switch (level)
                {
                    case 0:
                        isAutomatic = false;
                        fireRate = 0.2f;

                        break;
                    case 1:
                        isAutomatic = true;
                        fireRate = 0.2f;
                        break;
                    case 2:
                        isAutomatic = true;
                        fireRate = 0.08f;
                        break;
                }
                break;

            case 1:

                usingShotgun = true;
                standardBullet = shotgunRound;
                isAutomatic = false;
                fireRate = 0.3f;
                switch (level)
                {
                    case 0:
                        shotgunCount = 6;
                        shotgunSpread = 0.4f;

                        break;
                    case 1:
                        shotgunCount = 8;
                        shotgunSpread = 0.3f;
                        break;
                    case 2:
                        shotgunCount = 10;
                        shotgunSpread = 0.15f;
                        break;
                }
                break;

            case 2:
                break;
        }

    }
}
