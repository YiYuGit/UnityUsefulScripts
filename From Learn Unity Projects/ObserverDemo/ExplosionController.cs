using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When called KilleMe(), the script destroy the attached gameobject 
/// </summary>

public class ExplosionController : MonoBehaviour
{
    public void KillMe()
    {
        Destroy(gameObject);
    }
}
