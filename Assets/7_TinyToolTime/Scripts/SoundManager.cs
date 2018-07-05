using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip[] Sounds;
    private AudioSource audioSource;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();

        //Sounds = (AudioClip[])Resources.LoadAll("7_TeamSuperCoolScene/Sounds"); Lieber nicht  ;)
        /*Sounds[0] = Resources.Load("axt") as AudioClip;
        Sounds[1] = Resources.Load("7_TeamSuperCoolScene/Sounds/eimer.wav") as AudioClip;
        Sounds[2] = Resources.Load("7_TeamSuperCoolScene/Sounds/trampolin.wav") as AudioClip;
        Sounds[3] = Resources.Load("7_TeamSuperCoolScene/Sounds/hit.wav") as AudioClip; ;
        Sounds[4] = Resources.Load("7_TeamSuperCoolScene/Sounds/truhe.wav") as AudioClip;
        Sounds[5] = Resources.Load("7_TeamSuperCoolScene/Sounds/win.wav") as AudioClip;             Erstmal gelassen hier... Resources.load will grad nich so recht und ich hab KA warum...
        Sounds[6] = Resources.Load("7_TeamSuperCoolScene/Sounds/magie.wav") as AudioClip; */
        ChooseSound(10);
 
    }


    public void PlaySound(AnimationEvent aniEvent)
    {
        int sound = aniEvent.intParameter;
        ChooseSound(sound);
    }

    public void ChooseSound (int sound)
    {

        switch (sound)
        {
            /* 1 : axt
             * 2 : eimer
             * 3 : trampolin
             * 4 : hit (Spieler rammt gegen den Baum oder fällt auf den Boden)
             * 5 : truhe
             * 6 : win (Wenn Buchstabe erscheint)
             * 7 : magie (Fragezeichen)
             * 8 : jeah
             * 9 : eimerbreak
             * 
             */
            case 1:
                audioSource.clip = Sounds[0];
                break;
            case 2:
                audioSource.clip = Sounds[1];
                break;
            case 3:
                audioSource.clip = Sounds[2];
                break;
            case 4:
                audioSource.clip = Sounds[3];
                break;
            case 5:
                audioSource.clip = Sounds[4];
                break;
            case 6:
                audioSource.clip = Sounds[5];
                break;
            case 7:
                audioSource.clip = Sounds[6];
                break;
            case 8:
                audioSource.clip = Sounds[7];
                break;
            case 9:
                audioSource.clip = Sounds[8];
                break;
            default:
                break;
        }
        audioSource.Play();



    }
}
