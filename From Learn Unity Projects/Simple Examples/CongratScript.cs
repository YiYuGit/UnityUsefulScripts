using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script loog through all the TextToDisplay in hte List<> with a count down timer
/// 
/// </summary>

public class CongratScript : MonoBehaviour
{
    public TextMesh Text;
    public ParticleSystem SparksParticles;

    //private List<string> TextToDisplay = new List<string>();

    public List<string> TextToDisplay;
    private float RotatingSpeed;
    private float TimeToNextText;

    private int CurrentText;

    // Start is called before the first frame update
    void Start()
    {
        TimeToNextText = 0.0f;
        CurrentText = 0;

        RotatingSpeed = 1.0f;

        TextToDisplay.Add("Congratulation");
        TextToDisplay.Add("All Errors Fixed");

        Text.text = TextToDisplay[0];

        SparksParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        TimeToNextText += Time.deltaTime;

        if (TimeToNextText > 1f)
        {

            Text.text = TextToDisplay[CurrentText];

            TimeToNextText = 0.0f;

            CurrentText++;


            if (CurrentText == (TextToDisplay.Count))
            {
                Text.text = TextToDisplay[CurrentText - 1];

                TimeToNextText = 0.0f;

                CurrentText = 0;
  
            }

        }
    }

}