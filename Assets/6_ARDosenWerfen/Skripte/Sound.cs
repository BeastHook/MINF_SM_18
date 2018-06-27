using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]  //lässt die Klasse, mithilfe des Audiomanagers, im Inspector als Array erscheinen
public class Sound {

    public AudioClip clip;

    // Die Attribute des Sounds
    public string name;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

}
