using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/// <summary>
/// This script opens url by combining preset url information and user input from inputfield
/// The "IEnumerator GetText()" try to get information from a url and show text reseult
///     private void Update() is for quiting the app.
///     
///     public void open() need to be linked to a button on the UI
///     
/// In this example, the script is linked to one button and two text inputfield,
/// the user input longitude and latitude number and combined with url to visit a coordinate converting service
/// the web request part used google.com as example
/// 
/// </summary>

public class OpenURL : MonoBehaviour
{
    public string url;



    public Text latText;
    public Text longText;



    public void open()
    {

        Debug.Log(latText.text);

        Debug.Log(longText.text);
        /*
        if (url != null)
        {
            Application.OpenURL(url + latText.text + "&y=" + longText.text);
        }
        */

        Debug.Log(url + latText.text + "&y=" + longText.text);
        Application.OpenURL(url + latText.text + "&y=" + longText.text);

        StartCoroutine(GetText());
    }



    IEnumerator GetText()
    {
        //UnityWebRequest www = UnityWebRequest.Get(url + latText.text + "&y=" + longText.text);
        UnityWebRequest www = UnityWebRequest.Get("https://www.google.com");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}
