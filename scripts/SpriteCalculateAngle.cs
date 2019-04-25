using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCalculateAngle : MonoBehaviour {

    public float spriteAngle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        spriteAngle = Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z));

        Vector3 cross = Vector3.Cross(new Vector2(transform.forward.x, transform.forward.z), new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z));

        if(cross.z > 0)
        {
            spriteAngle = 360 - spriteAngle;
        }


       // Debug.Log(spriteAngle);
        
	

	}

   
}

