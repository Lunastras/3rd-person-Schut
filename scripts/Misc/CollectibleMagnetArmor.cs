using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMagnetArmor : MonoBehaviour {

    public bool isFollowPlayer = false;
    public Transform player;
    public float speed;
    public BoxCollider cube;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isFollowPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.name.Equals("Player"))
        {


            GetComponent<Rigidbody>().isKinematic = true;
            isFollowPlayer = true;
            player = other.transform;


            cube.GetComponent<BoxCollider>().isTrigger = true;


        }
    }
}
