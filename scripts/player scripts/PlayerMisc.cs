using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMisc : MonoBehaviour {



   

    public int armorMax;
    public int hpMax;
    public int playerHP = 100;
    public int playerArmor = 50;
    public int[] weaponLevel = new int[5];


    public int currentWeapon;
    public int numberOfWeapons;
    public int expRequiredForLevel;
    private int maxLevel = 3;
    bool backSwitchRaised = true;
    bool frontSwitchRaised = true;

    public GameObject bulletText;
    public GameObject healthBar;
    public GameObject armorBar;
    public GameObject levelBar;

    public AudioClip hpPickUpNoise;
    public AudioClip armorPickUpNoise;
    public AudioClip expPickUpNoise;
    public AudioClip levelUpNoise;
    private AudioSource audio;


    private GunControl gun;
    public GameObject genericBullet;

    private struct Weapon
    {
        public int level;
        public int exp;
    }




    Weapon[] weapon;

    // Use this for initialization
    void Start () {

        
        gun = GetComponent<GunControl>();
       

        audio = GetComponent<AudioSource>();
        healthBar = GameObject.Find("HealthBar");
        armorBar = GameObject.Find("ArmorBar");
        levelBar = GameObject.Find("LevelBar");

        weapon = new Weapon[4];
        weapon[currentWeapon].exp = 0;
        weapon[currentWeapon].exp = 0;
        gun.setGun(currentWeapon, weapon[currentWeapon].level);

        setLevelBar();
        setHealthBar();
        setArmorBar();
		
	}
    
	
	// Update is called once per frame
	void Update () {
    
        

        if (Input.GetAxisRaw("WeaponChange") > 0 && frontSwitchRaised)
        {
            frontSwitchRaised = false;
            currentWeapon++;
            if (currentWeapon == numberOfWeapons)
            {
                currentWeapon = 0;
            }

            gun.setGun(currentWeapon, weapon[currentWeapon].level);
            setLevelBar();
        }
        else if (Input.GetAxisRaw("WeaponChange") < 0 && backSwitchRaised)
        {
            backSwitchRaised = false;
            currentWeapon--;
            if (currentWeapon < 0)
            {
                currentWeapon = numberOfWeapons - 1;
            }

            gun.setGun(currentWeapon, weapon[currentWeapon].level);
            setLevelBar();
        }
        else
        if(Input.GetAxisRaw("WeaponChange") == 0)
        {
            backSwitchRaised = true;
            frontSwitchRaised = true;
        }
       

        bulletText.GetComponent<Text>().text = (currentWeapon+1).ToString() + "/" + numberOfWeapons.ToString();




    }


    public void playerDeath()
    {
        Debug.Log("oof");
    }

    public void getArmor(int armorPoints)
    {
        audio.PlayOneShot(armorPickUpNoise);
        playerArmor += armorPoints;
        playerArmor = Mathf.Clamp(playerArmor, 0, armorMax);
        FloatingTextController.createFloatingText(armorPoints.ToString(), transform, "PlayerArmorText");

    }



    public void damageGet(int damage)
    {

        int x = currentWeapon;

        if (playerArmor != 0)
        {
            playerHP -= (int)((float)damage * 0.6);
            playerArmor -= (int)((float)damage * 0.9);

            playerArmor = Mathf.Clamp(playerArmor, 0, armorMax);
            setArmorBar();
        }
        else
        {
            playerHP -= damage;
        }

        playerHP = Mathf.Clamp(playerHP, 0, hpMax);

        if (playerHP == 0)
        {
            playerDeath();
        }

        weapon[x].exp -= damage;

        if(weapon[x].exp < 0)
        {
            if(weapon[x].level > 0)
            {
                //we use this while in the case that the damage dealt is bigger than an entire level
                while(weapon[x].exp < 0)
                {
                    weapon[x].level--;
                    weapon[x].exp = expRequiredForLevel + weapon[x].exp;
                }
                
            }
            else
            {
                weapon[x].exp = 0;
            }

            gun.setGun(currentWeapon, weapon[currentWeapon].level);

        }


        setLevelBar();
        setHealthBar();

    }

    public void getHP(int hp)
    {
        playerHP += hp;
        playerHP = Mathf.Clamp(playerHP, 0, hpMax);
        setHealthBar();

        Debug.Log("Yeeeeeeeeeee more hppphp");

        audio.PlayOneShot(hpPickUpNoise);
       
        FloatingTextController.createFloatingText(hp.ToString(), transform, "PlayerHealingText");
    }

    private void setHealthBar()
    {
        float hpDif = (float)playerHP / hpMax;
        healthBar.GetComponent<Image>().fillAmount = hpDif;
    }

    private void setArmorBar()
    {
        float armorDif = (float)playerArmor / armorMax;
        armorBar.GetComponent<Image>().fillAmount = armorDif;

    }

    public void getExp(int expValue)
    {
        //we use x just in case the weapon is switched as the program is running
        int x = currentWeapon;
        weapon[x].exp += expValue;
        
        if(weapon[x].exp >= expRequiredForLevel)
        {
            if (weapon[x].level + 1 < maxLevel)
            {
                audio.PlayOneShot(levelUpNoise);
                while ((weapon[x].exp >= expRequiredForLevel) && (weapon[x].level + 1 < maxLevel))
                {
                    FloatingTextController.createFloatingText("Level up!", transform, "PlayerLevelUpText");


                    weapon[x].exp -= expRequiredForLevel;
                    weapon[x].level++;
                }

            }
            else
            {
                audio.PlayOneShot(expPickUpNoise);
            }

        }
        else
        {
            audio.PlayOneShot(expPickUpNoise);
        }


        if (weapon[x].exp > expRequiredForLevel)
        {
            weapon[x].exp = expRequiredForLevel;
        }

        gun.setGun(currentWeapon, weapon[currentWeapon].level);
        setLevelBar();

    }

    private void setLevelBar()
    {
        levelBar.GetComponent<Image>().fillAmount = (float)weapon[currentWeapon].exp / expRequiredForLevel;
    }

    
}
