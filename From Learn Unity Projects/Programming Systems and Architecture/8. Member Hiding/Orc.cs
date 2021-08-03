using UnityEngine;
using System.Collections;

/// <summary>
/// Orc script
/// </summary>
public class Orc : Enemy
{
    //This hides the Enemy version.
    new public void Yell()
    {
        Debug.Log("Orc version of the Yell() method");
    }
}