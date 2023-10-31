using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioClip[] objsound;

    private AudioSource source => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, bool destroy = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        source.pitch = Random.Range(p1, p2);
        source.PlayOneShot(clip, volume);
    }
}
