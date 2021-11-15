using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script track the location of tool/hand controller object, and then place the detector object below it.
/// This script should be attached to th detector object.
/// when in contact with the "pipe" object. It will trigger the sound.
/// 
/// Note: the detector tool itself need to have a trigger collider and rigidbody(turn off gravity)
/// </summary>


public class UtilityDetector : MonoBehaviour
{
    // The tool object, user will hold in hand(or on desktop version, an obejct that move with user)
    public GameObject tool;

    // The detecting depth of the detector from the tool location
    public float detectDepth;

    //Update location wait time
    public float updateTime = 0.05f;

    //The beeping sound drop here
    AudioSource m_MyAudioSource;

    //This is used to tune the pitch of the sound
    public float pitchDistance = 3f;
    private float pitch;


    private IEnumerator coroutine;
    void Awake()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        coroutine = UpdateLoaction(updateTime);

        StartCoroutine(coroutine);
    }

    // Update is called once per frame
   
    
    void FixedUpdate()
    {
        //Keep updating the location of the detector object
        //transform.position = new Vector3(tool.transform.position.x, tool.transform.position.y - detectDepth, tool.transform.position.z);
    }
    private IEnumerator UpdateLoaction(float updateTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTime);
            transform.position = new Vector3(tool.transform.position.x, tool.transform.position.y - detectDepth, tool.transform.position.z);

        }
    }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("pipe"))
        {
            if (!m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Play();

            }

            // While beeping, the closer the pipe and detector get, the higher the pitch
            pitch = 1/(Vector3.Distance(transform.position, other.transform.position)/pitchDistance);

            m_MyAudioSource.pitch = pitch;

        }
    }

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
