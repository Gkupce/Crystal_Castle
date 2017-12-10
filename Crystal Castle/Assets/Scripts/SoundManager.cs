using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip[] clips;

    AudioSource aSrc;
    static SoundManager instance;

	// Use this for initialization
	void Start () {
        instance = this;
        aSrc = GetComponent<AudioSource>();
	}
	
    public static void PlayClip(int index)
    {
        instance.aSrc.pitch = 1;
        instance.aSrc.PlayOneShot(instance.clips[index]);
    }
    public static void PlayClip(int index,float minPitch,float maxPitch)
    {
        instance.aSrc.pitch = Random.Range(minPitch,maxPitch);
        instance.aSrc.PlayOneShot(instance.clips[index]);
    }
}
