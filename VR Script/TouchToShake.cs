using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// This script is attached to SteamVR controller object, it will track the distance between 'pipe' object, and will trigger sound or shake based on the distance
/// It will also shake when the controller collide with 'car' object
/// 
/// </summary>

public class TouchToShake : MonoBehaviour
{

    public GameObject pipe;
    public float soundsensingDistance;
    public float shakesensingDistance;

    AudioSource m_MyAudioSource;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Shake()
    {
        Controller.TriggerHapticPulse(3500);
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("car"))
        {
            Shake();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            Shake();
        }
    }



    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, pipe.transform.position) < soundsensingDistance)
        {
            //m_MyAudioSource.Play();

            if (!m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Play();

            }

        }

        if (Vector3.Distance(transform.position, pipe.transform.position) >= soundsensingDistance)
        {
            //m_MyAudioSource.Play();

            if (m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Stop();

            }

        }


        if (Vector3.Distance(transform.position, pipe.transform.position) < shakesensingDistance)
        {
           
            Shake();
        }

    }


    // Start is called before the first frame update
    void Update()
    {
        
        if (Controller.GetHairTriggerDown())
        {

            Shake();

        }
        
       
    }
}

