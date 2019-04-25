using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static GameObject canvas;

    public static void initialize(string requiredText)
    {
        canvas = GameObject.Find("Canvas");
        popupText = Resources.Load<FloatingText>("Prefabs/UI/" + requiredText);

    }

    public static void createFloatingText(string text, Transform location, string requiredText)
    {
        initialize(requiredText);
        FloatingText instance = Instantiate(popupText);
        instance.origin = location;
        instance.hasOrigin = true;
        instance.transform.SetParent(canvas.transform, false);

        
        instance.SetText(text);
    }

}
