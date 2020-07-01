using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;        //A reference to the audio manager, so we can access it statically.
    public Sound[] sounds;                      //A reference to the array containing the music and sound effects

    void Awake()
    {
        //Let's make it 'simpleton'! (Did you get it?!)
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //Initializes the Sound array elements
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.soundClip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    public void PlayMusic(string musicName)
    {
        //finds the music...
        Sound s = Array.Find(sounds, sound => sound.soundName == musicName);
        if (s == null)
        {
            {
                Debug.LogError("Sound nof found!");
                return;
            }
        }
        //... and plays it.
        s.audioSource.Play();
    }

    public void StopMusic(string musicName)
    {
        //finds the music...
        Sound s = Array.Find(sounds, sound => sound.soundName == musicName);
        if (s == null)
        {
            Debug.LogError("Sound nof found!");
            return;
        }
        //... andstops it.
        s.audioSource.Stop();
    }
}
