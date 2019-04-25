using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMagnet : MonoBehaviour {

    public bool isFollowPlayer = false;
    public Transform player;
    public float speed;
    public SphereCollider orb;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(isFollowPlayer)
        {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {

       
        if(other.name.Equals("Player"))
        {
            Debug.Log("HEEEEEEE");
          
            GetComponent<Rigidbody>().isKinematic = true;
            isFollowPlayer = true;
            player = other.transform;

            
            orb.GetComponent<SphereCollider>().isTrigger = true;

        
        }
    }
}