using UnityEngine.Audio;
using UnityEngine;

// to be seen in unity
[System.Serializable]
public class Sound 
{
    // public variables
    public string name;

    // reference to the audio clip
    public AudioClip clip;

    [Range(0f,1f)]
    public float Volume;

    [Range(.1f,3f)]
    public float pitch;

    // won't be displayed in the inspector even tho it is public
    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
