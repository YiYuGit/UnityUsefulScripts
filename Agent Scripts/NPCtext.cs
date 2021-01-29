using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is attached to NPC object, under NPC object, put the NPC text, drag and drop the text and player into the slot
/// The script will detect the distance between NPC and player, when the distance is smaller than the approach distance, the text will change color and facing the player
/// </summary>
public class NPCtext : MonoBehaviour
{
    //The text object, remember to put the text Anchor to 'middle center'.

    public GameObject text;
    //Player object

    public GameObject player;

    //Approaching distance
    public float approachDist = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (text != null && dist < approachDist)
        {
            // Let the text facing the player, and change color

            text.transform.LookAt(player.transform.position);
            text.transform.Rotate(0, 180, 0);
            text.GetComponent<TextMesh>().color = Color.blue;
        }
    }
}
