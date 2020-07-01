using UnityEngine;

[System.Serializable]              //So we can tweak it in the Unity editor
public class Sound
{
    public string soundName;       //The name of the sound/music to be played
    public AudioClip soundClip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;                //Should the sound be played indefinitely?

    [HideInInspector]
    public AudioSource audioSource;
}
