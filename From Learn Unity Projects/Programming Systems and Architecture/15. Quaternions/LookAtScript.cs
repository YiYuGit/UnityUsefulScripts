using UnityEngine;
using System.Collections;

/// <summary>
/// On update, the script calculate the relative position bewtween this and target, change this rotation to the look rotation.
/// </summary>


public class LookAtScript : MonoBehaviour
{
    public Transform target;


    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }
}