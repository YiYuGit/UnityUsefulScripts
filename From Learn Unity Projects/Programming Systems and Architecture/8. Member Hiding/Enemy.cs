using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy script with Yell() method
/// </summary>


public class Enemy : Humanoid
{
    //This hides the Humanoid version.
    new public void Yell()
    {
        Debug.Log("Enemy version of the Yell() method");
    }
}