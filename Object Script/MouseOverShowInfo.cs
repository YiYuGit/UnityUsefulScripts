using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
///  This script test the OnMouseOver() function. 
///  When mouse over, two world space text objects are turned on or created to show the hidden info.
///  When mouse out, they disappear or destroyed.
/// </summary>

public class MouseOverShowInfo : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;

    private LineRenderer lineRenderer1;
    //private LineRenderer lineRenderer2;

    private void OnMouseOver()
    {
        text1.gameObject.SetActive(true);
        text2.gameObject.SetActive(true);

        //lineRenderer1.sharedMaterial.SetColor("_Color", Color.gray);

        //lineRenderer1.startColor = Color.white;
        //lineRenderer1.endColor = Color.white;

        lineRenderer1.startWidth = 4f;
        lineRenderer1.endWidth = 4f;
        lineRenderer1.positionCount = 4;
        lineRenderer1.SetPosition(0, this.transform.position);
        lineRenderer1.SetPosition(1, text1.transform.position);

        lineRenderer1.SetPosition(2, this.transform.position);
        lineRenderer1.SetPosition(3, text2.transform.position);

    }

    private void OnMouseExit()
    {
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);

        lineRenderer1.positionCount = 0;


    }


    // Start is called before the first frame update
    void Start()
    {
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);

        lineRenderer1 = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
