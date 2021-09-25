using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to NPC object, under NPC object, put the NPC text, drag and drop the text and player into the slot
/// The script will detect the distance between NPC and player, when the distance is smaller than the approach distance, the text will change color and facing the player
/// </summary>


public class NPCtext : MonoBehaviour
{
    //The text object, remember to put the text Anchor to 'middle center', so the text rotate around the center
    public GameObject text;

    //Player object drop here
    public GameObject player;

    //Approaching distance between NPC and player
    public float approachDist = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Turn off text on start
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between player and NPC
        float dist = Vector3.Distance(player.transform.position, transform.position);

        // If text is not empty and the distance is smaller than the approach distance
        if (text != null && dist < approachDist)
        {
            //Active the text
            text.SetActive(true);

            // Let the text facing the player, and change color, 
            //text.transform.LookAt(player.transform.position);      // This option look at player x y z.

            //This option only look at on Y axis to keep text straight 
            Vector3 targetPostition = new Vector3(player.transform.position.x, text.transform.position.y, player.transform.position.z);
            text.transform.LookAt(targetPostition);

            // Rotate the text once moreto get correct view
            text.transform.Rotate(0, 180, 0);

            // Change the text color
            text.GetComponent<TextMesh>().color = Color.blue;
        }

        if (text == null || dist > approachDist)
        {
            //If text is empty or player is far away, turn off text
            text.SetActive(false);
        }
    }
}
