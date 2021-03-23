using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach both script and audio source to game object
/// Make sure the object has collider
/// The script will find the audio source on start and disable the loop. 
/// When trigger or collision happen, it will play the sound.
/// </summary>

public class TriggerSoundPlay : MonoBehaviour
{

    // Put the audio source here
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.loop = false;
    }


    void OnTriggerEnter(Collider other)
    {
        //sound.Play();
    }


    void OnCollisionEnter(Collision collision)
    {
        sound.Play();
    }
}
