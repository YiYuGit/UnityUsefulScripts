using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used for instantiating  prefab image object at user selected location with corresponding position and rotation.
/// So the result is apply paint image to surface.
/// </summary>



public class ApplyPaintToSurface : MonoBehaviour
{

    public GameObject main;
    public GameObject[] image;
    public AudioSource m_MyAudioSource1;
    public AudioSource m_MyAudioSource2;

    public GameObject[] sign;

    //AudioSource m_MyAudioSource;
    private GameObject selectedImage;
    private int i = 0;


    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //m_MyAudioSource = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < sign.Length; j++)
        {
            sign[j].SetActive(false);
        }


    }

    public void OnTriggerStay(Collider other)
    {


        if(other.tag == "mark")
        {
            Debug.Log("hit");

            //if (Controller.GetHairTriggerDown())
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {

                Debug.Log("trigger1");
                Destroy(other.gameObject);

                /*
                float startTime = Time.time;

                if(Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger)&& Mathf.Abs(Time.time - startTime)>2f)
                {
                    Debug.Log("trigger2");

                    Destroy(other.gameObject);
                }
                */
            }
        }
    }



    // Update is called once per frame
    void Update()
    {


        // Press application menu button to switch between different paint image
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            m_MyAudioSource2.Play();

            for (int j = 0; j < sign.Length; j++)
            {
                sign[j].SetActive(false);
            }


            selectedImage = image[i];
            //Also active the current selected image sign on controller
            sign[i].SetActive(true);

            if (i < image.Length)
            {
                i++;
            }

            if (i >= image.Length)
            {
                i = 0;
            }


        }






            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 9;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            //Debug.Log("HitPoint is" + hit.point);
            //Debug.Log("HitPoint normal is" + hit.normal);

            //Debug.Log("C.Rotation" + main.transform.rotation.x + ", " + main.transform.rotation.y + ", " + main.transform.rotation.z);
            //Debug.Log("C.Rotation Euler" + Quaternion.Euler(main.transform.rotation.x, main.transform.rotation.y, main.transform.rotation.z));

            //if (Input.GetMouseButtonDown(0))
            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                // Make a new Quaternion rotation according to hit.normal, this make the new image in next step attach to hit point and parallel
                Quaternion rotation = Quaternion.FromToRotation((new Vector3(0, 1, 0)), hit.normal);

                // Instantiate new image , that is "paint" on surface
                GameObject a = Instantiate(selectedImage, hit.point, rotation);

                // Rotate newly instantiated image to player's direction, or maybe in VR, the controller's direction.
                a.transform.Rotate(0, main.transform.eulerAngles.y, 0, Space.Self);


                // Make spray paint sound
                if (!m_MyAudioSource1.isPlaying)
                {
                    m_MyAudioSource1.Play();

                }


            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
            
        }

    }
}
