using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// The script is attached to one object that control the active true or false status of other object.
/// If you want to control multiple game obejct. Modify the code GameObject to GameObject[]
/// or put all objects in one empty gameobject.
/// 
/// This solution may not be the best to use if you also need access to the obejects after their active stative is set to false.
/// In that case, try to use 
/// 
/// public Renderer rend;
///  
/// rend = GetComponent<Renderer>();
/// rend.enabled = true/false;
/// 
/// </summary>
public class SetActiveTF : MonoBehaviour
{
    [Header("Normal Object1")]
    public GameObject targertObj1;
    [Header("Normal Object2")]
    public GameObject targertObj2;

    [Header("Object with child objects")]
    public GameObject targertObj3;



    public bool objActiveStatus = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchActive();
        }
    }

    public void SwitchActive()
    {
        targertObj1.SetActive(!objActiveStatus);
        targertObj2.SetActive(!objActiveStatus);

        //turn off/ on the render of the dots so the user won't see
        foreach (Renderer r in targertObj3.GetComponentsInChildren<Renderer>())
        {
            r.enabled = !r.enabled;
        }

        // turn off / on the sphere collider so the user won't accidentally click on them
        foreach (SphereCollider s in targertObj3.GetComponentsInChildren<SphereCollider>())
        {
            s.enabled = !s.enabled;
        }

        objActiveStatus = !objActiveStatus;

    }


}
