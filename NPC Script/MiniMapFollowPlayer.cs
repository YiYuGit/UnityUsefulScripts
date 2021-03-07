using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This tutorial is from https://www.youtube.com/watch?v=28JTTXqMvOU
/// To make a mini map:
/// (1) Make a secondary camera, rename it MiniMapCamera, put it above player in scene, rotate it 90deg in x axis. The projection mode usually should be Othrographic
/// (2) Make a new Canvas, inside it, make a new Raw Image, resize and reposition it.
/// (3) Make a new RenderTexture, resize it to fit the (2) Raw Image, and in MiniMapCamera drag and drop the RenderTexture in Target Texture.
/// (4) Put this script on the MiniMapCamera and reference the player.
/// This script mainly update the position and/or roation of the camera, so MiniMapCamera can follow player.
/// </summary>
public class MiniMapFollowPlayer : MonoBehaviour
{
    public Transform player;


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = player.position;

        newPosition.y = transform.position.y;

        transform.position = newPosition;

        // If mini map rotation is also needed
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
