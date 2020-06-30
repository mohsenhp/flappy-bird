using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
	public Sound[] sounds;

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

		//Initializes the sounds array
		foreach (Sound s in sounds)
		{
			s.audioSource = gameObject.AddComponent<AudioSource>();
			s.audioSource.clip = s.soundClip;
			s.audioSource.volume = s.volume;
			s.audioSource.pitch = s.pitch;
			s.audioSource.loop = s.loop;
		}
			
	}

	public void PlayMusic (string musicName)
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
