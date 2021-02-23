using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // need to import TextMeshPro from Unity-Window-TextMeshPro


/// <summary>
/// This script partially come from https://www.youtube.com/watch?v=f-oSXg6_AMQ
/// This is attached to an empty gameobject to make a dialog manager, from where user input sentences
/// and drag drop the text and from canvas
/// the "continueButton" is a button on canvas that is used for continuing the dialog by user click
/// This scrpit can be extented by adding sound and animation.
/// 
/// </summary>
public class DialogUI : MonoBehaviour
{

    // The text on canvas
    public TextMeshProUGUI textDisplay;

    // The sentences of the dialog
    public string[] sentences;

    private int index;

    // The time between two letters
    public float typingSpeed;

    // The button for continuing the dialog
    public GameObject continueButton;

    IEnumerator Type()
    {
        // Typing letter by letter in each sentence
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        // Turn off the continue button when typing
        continueButton.SetActive(false);

        if (index < sentences.Length -1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        // Turn on continue button when one sentence is typed
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
}
