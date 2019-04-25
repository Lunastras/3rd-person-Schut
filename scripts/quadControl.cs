using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadControl : MonoBehaviour {




    public Texture[] sprite = new Texture[10];
    public float animationSpeed;

    bool HasChangedFrame = true;

    bool needsToBeInverted = false;
    int spriteRot;
    public int frame = 0;



	// Use this for initialization
	void Start () {

  
    }
	
	// Update is called once per frame
	void Update () {




        float angle = GetComponentInParent<SpriteCalculateAngle>().spriteAngle;

        spriteRot = SpriteAngle(angle);



        if(needsToBeInverted && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if(transform.localScale.x < 0 && !needsToBeInverted)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }



        GetComponent<Renderer>().material.mainTexture = sprite[spriteRot*2 + frame];

        if(HasChangedFrame)
            StartCoroutine(FrameChange(animationSpeed));

        //Debug.Log(spriteRot);




    }

    public int SpriteAngle(float angle)
    {
        if (angle >= 292.5f && angle < 337.5f) //FrontRight
        {
            needsToBeInverted = true;
            return 1;
        }
            
        else 
        if (angle >= 22.5f && angle < 67.5f) //FrontLeft*
        {
            needsToBeInverted = false;
            return 1;
        }
            
        else 
        if (angle >= 67.5f && angle < 112.5f) //Left*
        {
            needsToBeInverted = false;
            return 2;
        }
            
        else
        if (angle >= 112.5f && angle < 157.5f) //BackLeft*
        {
            needsToBeInverted = false;
            return 3;
        }
            
        else 
        if (angle >= 157.5f && angle < 202.5f) //Back
        {
            needsToBeInverted = false;
            return 4;
        }
            
        else 
        if (angle >= 202.5f && angle < 247.5f) //BackRight
        {
            needsToBeInverted = true;
            return 3;
        }
            
        else
        if (angle >= 247.5f && angle < 292.5f) //Right
        {
            needsToBeInverted = true;
            return 2;
        }
            
        else if (angle >= 337.5f || angle < 22.5f) //front
        {
            needsToBeInverted = false;
            return 0;
        }
            

        

        else return 0;
    }

    private IEnumerator FrameChange(float delay)
    {
        HasChangedFrame = false;

        yield return new WaitForSeconds(delay);

        HasChangedFrame = true;
        frame++;
        // Debug.Log(frame);
        frame %= 2;


    }


}
