using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
    public bool playerIsBusy;
    private GameObject camera;
    public Transform target;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerMovement player;
    public bool IsActive;
    public int schpool;



    //typing variables
    private bool isTyping = false;
    private bool cancelTyping = false;
    public float typeSpeed;

    public bool stopPlayerMovement; 


	// Use this for initialization
	void Start () {

        camera = Camera.main.gameObject;

        if ( textFile != null)
        {
            player = FindObjectOfType<PlayerMovement>();

            textLines = (textFile.text.Split('\n'));
        }

       /* if( endAtLine == 0)
        {
            endAtLine = textLines.Length - 1; 
        }*/

        if (IsActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!IsActive)
        {
            return;

        }

        camera.transform.LookAt(target);


        // theText.text = textLines[currentLine];



        if (Input.GetAxisRaw("Interract") > 0)
        {
            if(player.keyWasRaised)
            {
                player.keyWasRaised = false;
                if (!isTyping)
                {
                    currentLine++;

                    if (currentLine > endAtLine)
                    {
                        DisableTextBox();
                    }
                    else
                    {
                        StartCoroutine(TextScroll(textLines[currentLine]));
                    }

                }
                else if (isTyping && !cancelTyping)
                {
                    cancelTyping = true;
                }

            }
           
           


        }
        else
        {
            player.keyWasRaised = true;
        }


    }

    private IEnumerator TextScroll (string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }

        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextBox()
    {
        camera.GetComponent<ThirdPersonCamera>().canMove = false;
        
        textBox.SetActive(true);
        IsActive = true;

        player.isTalking = true;

        if (stopPlayerMovement)
        {
            player.canMove = false;
        }

        currentLine = schpool;

        StartCoroutine(TextScroll(textLines[currentLine]));

    }

    public void DisableTextBox()
    {
        camera.GetComponent<ThirdPersonCamera>().canMove = true;
        textBox.SetActive(false);
        IsActive = false;

        player.isTalking = false;
        player.canMove = true;
        
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }

   

}
