using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScriptAtLine : MonoBehaviour
{
    public PlayerMovement player;
    public TextAsset theText;
    bool waitForPress;
    public bool requireButtonPress;
    public int startLine;
    public int endLine;
    public bool stopMovement;
    bool canInterract;


    public TextBoxManager theTextBox;
    public bool destroyWhenActivated;

	// Use this for initialization
	void Start ()
    {

        theTextBox = FindObjectOfType<TextBoxManager>();

        player = FindObjectOfType<PlayerMovement>();


    }
	
	// Update is called once per frame
	void Update ()
    {
	
        
        if(Input.GetAxis("Interract") > 0)
        {

        
            if(waitForPress && player.keyWasRaised && !player.isTalking)
            {
                player.keyWasRaised = false;
                theTextBox.ReloadScript(theText);
                theTextBox.stopPlayerMovement = stopMovement;

                theTextBox.schpool = startLine;

                theTextBox.EnableTextBox();
                theTextBox.currentLine = startLine;
                theTextBox.endAtLine = endLine;
           



                if (destroyWhenActivated)
                {
                   Destroy(gameObject);
                }
            }
        }
        else
        {
            player.keyWasRaised = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {

            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }


            theTextBox.target = gameObject.transform;
            theTextBox.ReloadScript(theText);  
            theTextBox.stopPlayerMovement = stopMovement;
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;



            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }



        }

    }


    private void OnTriggerExit(Collider other)
    {
       if( other.name == "Player")
        {
            waitForPress = false;
        }
    }

}
