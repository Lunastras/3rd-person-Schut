using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {


    public Animator animator;
    private Text damageText;
    public bool hasOrigin;
    public Transform origin;
    Vector3 lastOrigin;
    float xOffset;
    float yOffset;

	// Use this for initialization
	void Start () {

       AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
       Destroy(gameObject, clipInfo[0].clip.length);

       xOffset = Random.Range(-20f, 20f);
       yOffset = Random.Range(-20f, 20f);


    }

    private void Update()
    {
        if(hasOrigin && (origin != null) )
        {
            setPosition(origin.position);
            lastOrigin = origin.position;

        }
        else
        {
            if (origin == null)
            {
                setPosition(lastOrigin);
            }
        }
       
       
    }

    private void setPosition(Vector3 location)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location);
        transform.position = new Vector2(screenPosition.x + xOffset, screenPosition.y + yOffset);

    }

    public void SetText(string text)
    {
        damageText = GetComponentInChildren<Text>();
        damageText.text += text;
    }

   
}
