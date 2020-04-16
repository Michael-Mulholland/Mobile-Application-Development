using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    // Sound array to hold the game sounds
    public Sound[] sounds;

    //public static AudioManager instance;

    // for initialization
    void Awake() {
        // loop through the sounds in the sounds array
        foreach(Sound s in sounds){
            // add the audio source component
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }    
    }
    
    // method to play sounds
    public void Play(string name){
        // find a sound in the Sounds array where the sound name is equal to the sound i have added
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        // don't play a sound that is not there
        if(s == null){
            Debug.LogWarning("Sound: " + name + " is not found!");
            return;
        }
        s.source.Play();
    }

    void Start() {
        // play the sound named LevelOneTheme
        Play("LevelOneTheme");    
    }
}
