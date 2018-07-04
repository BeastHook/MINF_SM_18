using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManagerDosenwerfen : MonoBehaviour {


    public Sound[] sounds;


	void Awake () {

        // fügt jedem Sound im Soundarray ein AudioSource Objekt  inkl. Attributen hinzu
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

        }
    }
	
	
	public void Play (string name)
    {
        // sucht im Array den richtigen SOund 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

	public void Stop (string name)
	{
		// sucht im Array den richtigen SOund 
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Stop();
	}

	public Sound getSound (string name){
		return Array.Find(sounds, sound => sound.name == name);
	}
}
