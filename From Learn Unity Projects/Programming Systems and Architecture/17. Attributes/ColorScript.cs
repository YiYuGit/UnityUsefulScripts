using UnityEngine;
using System.Collections;

/// <summary>
/// Change color of material
/// </summary>

[ExecuteInEditMode]
public class ColorScript : MonoBehaviour
{
    void Start()
    {
        renderer.sharedMaterial.color = Color.red;
    }
}