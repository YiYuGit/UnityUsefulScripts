using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script track the location of tool/hand controller object, and then place the detector object(with trigger collider) below it.
/// This script should be attached to th detector object.
/// when in contact with the "pipe" object. It will trigger the beeping sound.
/// 
/// Note: the detector tool itself need to have a trigger collider and rigidbody(turn off gravity)
/// During the contacting time of the detector and pipe object, there will be a beeping sound,
/// and the pitch of the sound is depended on the distance of the two object, the closer they get,the higher the pitch of sound.
/// </summary>


public class UtilityDetector : MonoBehaviour
{
    // The tool object, user will hold in hand(or on desktop version, an obejct that move with another object on player controller)
    public GameObject tool;

    // The depth between center of tool object to the center of the detector collider object
    public float detectDepth;

    //Update location wait time, example:0.05 means 20 times per second
    public float updateTime = 0.05f;

    //The beeping sound file drop here
    AudioSource m_MyAudioSource;

    //This is used to tune the pitch of the sound
    // pitchDistance usually should be equal to the detector cylinder collider's radius
    public float pitchDistance = 3f;
    // the number to be set to audio.pitch
    private float pitch;

    // The coroutine to be used to update detector object's location
    private IEnumerator coroutine;

    void Awake()
    {
        //Get the AudioSource on Awake
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set the time for coroutine
        coroutine = UpdateLoaction(updateTime);

        //Start running
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
   
    
    void FixedUpdate()
    {
        //Using Update is not working properly.So switched to IEnumerator
        //Keep updating the location of the detector object
        //transform.position = new Vector3(tool.transform.position.x, tool.transform.position.y - detectDepth, tool.transform.position.z);
    }

    // IEnumerator used to keep updating the location of the detector
    private IEnumerator UpdateLoaction(float updateTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTime);
            // Follow the "tool" position, but on the y axis, minus the detectDepth, usually should be set to 1/2 of the real height of detector collider
            transform.position = new Vector3(tool.transform.position.x, tool.transform.position.y - detectDepth, tool.transform.position.z);

        }
    }

    // OnTriggerEnter, if hitting the pipe object, and if the beeping sound is not playing, play the beeping sound.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pipe"))
        {
           // pitchDistance = Vector3.Distance(transform.position, other.transform.position);

            if (!m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Play();

            }
        }
    }

    // OnTriggerStay, if hitting the pipe object, and if the beeping sound is not playing, play the beeping sound.
    // Also, changing the pithch of sound
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("pipe"))
        {
            if (!m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Play();

            }

            // While beeping, the closer the pipe and detector get, the higher the pitch
            // Method 1: using the distance between the two objects, but this also included the y axis distance
            //pitch = 1/(Vector3.Distance(transform.position, other.transform.position)/pitchDistance);

            // Method 2: 
            var soundDistance = transform.position - other.transform.position;
            soundDistance.y = 0;
            pitch = 1/(soundDistance.magnitude/pitchDistance);

            m_MyAudioSource.pitch = pitch;

        }
    }

    // When exiting the pipe object, if the beeping sound is playing, stop playing 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("pipe"))
        {
            if (m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Stop();
            }
        }
    }


}
