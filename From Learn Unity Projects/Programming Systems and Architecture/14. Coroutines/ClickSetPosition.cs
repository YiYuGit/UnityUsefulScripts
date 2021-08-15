using UnityEngine;
using System.Collections;

/// <summary>
/// OnMouseDown if raycast hit a game object, take that hit point postion plus a vector3 to give to coroutineScript's target
/// </summary>
public class ClickSetPosition : MonoBehaviour
{
    public PropertiesAndCoroutines coroutineScript;


    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        if (hit.collider.gameObject == gameObject)
        {
            Vector3 newTarget = hit.point + new Vector3(0, 0.5f, 0);
            coroutineScript.Target = newTarget;
        }
    }
}