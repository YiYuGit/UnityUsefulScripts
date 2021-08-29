using UnityEngine;
using System.Collections;

/// <summary>
/// Change random color
/// </summary>
public class TurnColorScript : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.OnClicked += TurnColor;
    }


    void OnDisable()
    {
        EventManager.OnClicked -= TurnColor;
    }


    void TurnColor()
    {
        Color col = new Color(Random.value, Random.value, Random.value);
        renderer.material.color = col;
    }
}