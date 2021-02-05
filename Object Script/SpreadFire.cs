using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script attach to a fire gameobject, when contact with "Wood" obejct, the fire can spread, and the fire have a self-destroy time
/// On self-destroy, there will be a burn mark on the ground of fire
/// Prefab is available in the FireObjectPack.unitypackage
/// </summary>
public class SpreadFire : MonoBehaviour {

    
    //Put the fire object prefab here
    public GameObject fireObject;

    //Put the burn makr object here
    public GameObject burnMark;
    
    //The time for fire to insantiate a new fire
    public float waitTime = 12f;

    //The time for fire to extinguish (self-destroy)
    public float extTime = 30f;
    
    //The waiting status of the generating new fire
    private bool waiting = false;


    void Start()
    {

    }


    //Make a new fire every waitTime seconds, when touching wood object.
    IEnumerator CountDown()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        //Make a new position new the current fire object
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0;
        float z = Random.Range(-0.5f, 0.5f);
        Vector3 pos = new Vector3(x, y, z);
        Vector3 position = transform.position + pos;

        //Instantiate the new fire object at the new position
        Instantiate(fireObject, position, transform.rotation);

        waiting = false;

    }



    //When collide wood object, start the countDown(), when collide with extinguisher/player, the fire destroy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        if ((other.gameObject.CompareTag("Wood"))& waiting == false)
        {
            StartCoroutine(CountDown());
        }
    }


    //If the fire is not generating new fire, the extTime will count down
    private void LateUpdate()
    {
        if (waiting == false)
        {
            Destroy(gameObject, extTime);
          
        }
    }


    //Draw burn marks on ground of the fire object
    void DrawMark()
    {
        float x1 = transform.position.x;

        //Y postion - half of collider's height
        float y1 = transform.position.y - 0.49f;

        float z1 = transform.position.z;
        Vector3 pos1 = new Vector3(x1, y1, z1);

        //Give the burn mark a random Y rotation so they down look the same
        Instantiate(burnMark, pos1, Quaternion.Euler(0, Random.Range(0f, 180f), 0));

        //Give the burn mark a slightly random size
        float scaleTemp = Random.Range(0.8f, 1f);

        burnMark.transform.localScale = new Vector3(scaleTemp, scaleTemp, scaleTemp);
    }


    // When fire destroy, call drawMark function
    void OnDestroy()
    {
        DrawMark();
    }
}
