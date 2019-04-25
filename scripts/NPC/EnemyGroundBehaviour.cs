using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundBehaviour : MonoBehaviour {

    //moving control
    public bool isChasing;
    public int speed;
    public int chaseSpeed;
    int currentSpeed;
    float currentWait;
    public float walkInterval;
    public float waitTime;
    public int rateToChaseTarget; //the probability of him following the player once every walk interval
    bool isWaiting;
    public int gravityScale;
    public int maxDistFromTarget;
    public int minDistFromTarget;
    public bool isAerial;
    public int rateToGoUp;



    public Transform target;
    Vector3 moveDir;

    //firing control
  
    bool fireIntervalStarted;
    public int fireInterval;
    public int bulletsToFire;
    bool isFiring = false;
    public float coolDown;
    public float buildUpTime;
    public GameObject bullet;
    int bulletsFired;
    Vector3 previousRotation;
    private Renderer _rend;

    //Audio control


    private AudioSource audio;
    public AudioClip fireNoise;
    
    

    

	// Use this for initialization
	void Start () {


        audio = GetComponent<AudioSource>();
        _rend = GetComponentInChildren<Renderer>();
        _rend.material.EnableKeyword("_EMISSION");
        
        StartCoroutine(Waiting());
        currentSpeed = speed;
        currentWait = waitTime;

    }

    // Update is called once per frame
    void Update () {


        moveDir = new Vector3(0, 0, 0);

        if (!isWaiting && !isFiring)
        {
            moveDir = transform.forward * currentSpeed;   
        }

        if(!isAerial)
        {
            moveDir.y += Physics.gravity.y * gravityScale;
        }
       

        GetComponent<CharacterController>().Move(moveDir * Time.deltaTime);

        if(isChasing)
        {         

            if(!fireIntervalStarted)
            {
                StartCoroutine(fireIntervalCoolDown());
            }

            if(isFiring)
            {
                looAtTarget();
            }

        }     

    }

          /*
          * the generic walking coroutines
          */

    private IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(Random.Range(currentWait - 0.5f, currentWait + 0.5f));

        calculateRotation();

        isWaiting = false;
        StartCoroutine(Walking());
    }

    private IEnumerator Walking()
    {
        yield return new WaitForSeconds(Random.Range(walkInterval - 1.3f, walkInterval + 1.3f));

        StartCoroutine(Waiting());
    }

        /*
         * The coroutines that make the firing segment
         */

    private IEnumerator fireStart()
    {
        StartCoroutine(fireFlash(2, 0, Color.red));

        bulletsFired = 0;
        isFiring = true;
        yield return new WaitForSeconds(buildUpTime);

        StartCoroutine(firing());
    }

    private IEnumerator firing()
    {
        if(bulletsFired < bulletsToFire)
        {
            bulletsFired++;
            GameObject bulletAux = Instantiate(bullet, transform.position, transform.rotation);
            audio.PlayOneShot(fireNoise);
            bulletAux.transform.LookAt(target);
            yield return new WaitForSeconds(coolDown);

            StartCoroutine(firing());

        }
        else
        {
            isFiring = false;
            fireIntervalStarted = false;
            transform.rotation = Quaternion.Euler(previousRotation);
        }
        
    }

    private IEnumerator fireIntervalCoolDown()
    {
        fireIntervalStarted = true;
        yield return new WaitForSeconds(Random.Range(fireInterval - 1.3f, fireInterval + 1.3f));
        
        StartCoroutine(fireStart());
    }

    public IEnumerator fireFlash(int flashRep, int flashCount, Color wantedColor)
    {
        flashCount++;

        _rend.material.SetColor("_EmissionColor", wantedColor);  
        yield return new WaitForSeconds(0.1f);

        _rend.material.SetColor("_EmissionColor", Color.black);
        yield return new WaitForSeconds(0.1f);

        if (flashCount < flashRep)
        {
            
            StartCoroutine(fireFlash(flashRep, flashCount, wantedColor));
        }
        
   
    }

        /*
         * misc to make the program look cleaner
         */
    private void getRandomRotation()
    {
        if(!isAerial)
        {
            transform.Rotate(0, Random.Range(0, 359), 0);
        }
        else
        {
            if(rateToGoUp > Random.Range(0,99))
            {
                transform.Rotate(Random.Range(180, 359), Random.Range(0, 359), 0);

            }
            else
            {
                transform.Rotate(Random.Range(0, 179), Random.Range(0, 359), 0);

            }

        }

    }

    private void calculateRotation()
    {
        int distFromTarget = 0;
        if (isChasing)
        {
            distFromTarget = (int)Vector3.Distance(transform.position, target.position);
        }

        if (((Random.Range(0, 99) <= rateToChaseTarget) && isChasing) || (distFromTarget > maxDistFromTarget))
        {
            if ((distFromTarget > minDistFromTarget))
            {
                looAtTarget();
            }
            else
            {

                getRandomRotation();

            }

        }
        else
        {
            getRandomRotation();
        }

        previousRotation = transform.rotation.eulerAngles;
    }

    private void looAtTarget()
    {
        transform.LookAt(target);
        Vector3 rot = transform.rotation.eulerAngles;
       // rot = new Vector3(0, rot.y, 0);
        transform.rotation = Quaternion.Euler(rot);
    }

    public void outOfCombat()
    {
        isChasing = false;
        currentSpeed = speed;
        currentWait = waitTime;
    }

    public void gotInCombat(Transform newTarget)
    {
        target = newTarget;
        currentWait = 0;
        currentSpeed = chaseSpeed;
        isChasing = true;
    }
}
