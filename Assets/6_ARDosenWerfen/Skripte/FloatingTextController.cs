using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{

    private static GameObject canvas;
    private static FloatingText popupText;

    // initialisere den PopupText
    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");

    }

    public static void CreateFloatingText(string text, Transform location)
    {

        FloatingText instance = Instantiate(popupText);

        instance.transform.SetParent(canvas.transform, false);

        // Text wird übergeben
        instance.SetText(text);

    }
}
