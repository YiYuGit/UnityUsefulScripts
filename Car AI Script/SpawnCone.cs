using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach this script to an empty game object, drop in the car and cone object, the script will randomly generate cones in front of car to make obstacles
/// </summary>
/// 
public class SpawnCone : MonoBehaviour
{
    // Put in the driving car, the cone prefab and choose drop cone time interval.

    public GameObject car;
    public GameObject cone;
    public float dropConeInterval = 4f;
    public bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DropCone");
    }


    IEnumerator DropCone()
    {
        while(alive)
        {

            yield return new WaitForSeconds(dropConeInterval);
            GameObject Temporary_Cone_Handler;

            //relative poistion from world space to car local space
            //Vector3 carLocation = car.transform.InverseTransformPoint(transform.position);
            //Vector3 coneLocation = new Vector3(carLocation.x, carLocation.y + 0.2f, carLocation.z - 15);
            float x = Random.Range(-3f, 3f);
            float z = Random.Range(14f, 18f);

            // transform from car local space to world space
            Vector3 coneLocation = car.transform.TransformPoint(x, 2f, z);
            //Vector3 coneLocation = new Vector3(car.transform.position.x, car.transform.position.y + 0.2f, car.transform.position.z-15);
            Quaternion coneRotation = Quaternion.Euler(-90,0,0);
            Temporary_Cone_Handler = Instantiate(cone, coneLocation, coneRotation) as GameObject;

            //Temporary_Cone_Handler = Instantiate(cone, coneLocation, this.transform.rotation) as GameObject;

            Destroy(Temporary_Cone_Handler, 20f);


        }

    }

}
