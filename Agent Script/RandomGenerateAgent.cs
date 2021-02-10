using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to an empty object, so it will generate agent at certain location and time interval
/// </summary>

public class RandomGenerateAgent : MonoBehaviour
{
    //Put the agent prefab here
    public GameObject agnet;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateAgent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator generateAgent()
    {
        while (true)
        {
            //The time between instantiate
            float seconds = Random.Range(2f, 5f);
        
            yield return new WaitForSeconds(3.0f);

            // Select random location
            float x = Random.Range(-14f, -2f);
            float y = 0.4f;
            float z = Random.Range(29f, 36f);
            Vector3 location = new Vector3(x, y, z);

            // Instantiate new object
            GameObject p1 = Instantiate(agnet);


            //Assign to new location
            p1.transform.position = location;
        }

    }
}
