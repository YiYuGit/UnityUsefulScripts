using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This tool use two gameobject to measure the direct distance between them (preferbly using two small spheres)
/// The distance is shown in the inpector and the TMP text 
/// 
/// This is measuring 3D distance, for 2D, move two gameobject to same plance or modify the code
/// </summary>


public class DistanceMeasureTool : MonoBehaviour
{

    [Header("Drop two objects here")]
    public GameObject g1;
    public GameObject g2;

    [Header("Drop TMP text here for showing distance")]
    public TMP_Text distText;

    [SerializeField]
    private float distance;

    public void DistanceCheck()
    {
        // Update the distance
        distance = Vector3.Distance(g1.transform.position, g2.transform.position);
        // Update the text
        distText.text = distance.ToString();
        // Update the distance text position
        distText.transform.position = new Vector3((g1.transform.position.x + g2.transform.position.x) / 2, (g1.transform.position.y + g2.transform.position.y) / 2, (g1.transform.position.z + g2.transform.position.z) / 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
    }

    //Depending on the use case, choose one type of draw gizmos, on selected  or not 
    //void OnDrawGizmosSelected()
    void OnDrawGizmos()
    {
        if (g1 != null && g2 != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(g1.transform.position, g2.transform.position);
            DistanceCheck();
        }
    }

}
