using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to instantiate the prefab objects on a grid.
/// The script read the boundary infomation and take the prefab.
/// The instantiate process is controlled by a F11 key. If needed, this can be changed to be triggered by other method or key.
/// </summary>
public class InstantiateObjOnGrid : MonoBehaviour
{
    // Drop the container object here to hold the newly instantiated prefabs
    public Transform container;
    // Drop the prefab here
    public GameObject prefab;
    //This is designed to work on the same height y plane. Can be modified to work on other plane or even 3d space
    public float yHeight;

    // Need to get the x and z range from direct input or other script. In this case, the number come from FindMaxAndMinInArray public static.
    //public FindMaxAndMinInArray positionRange;

    // The spacing is will be * with the col and row number
    public float spacing;

    // The key to press to instantiate the prefabs
    public KeyCode instantKey = KeyCode.F11;


    void InstantiateObj()
    {
        // Get z and x range. z for row, x for column 
        int zRange = (int)(FindMaxAndMinInArray.zMax - FindMaxAndMinInArray.zMin);

        int xRange = (int)(FindMaxAndMinInArray.xMax - FindMaxAndMinInArray.xMin);

        // Two for loop to go through each point in the grid
        for ( int row = 0; row <= zRange + 1; row ++)
        {
            for (int col = 0; col <= xRange + 1; col ++)
            {
                // Instantiated the prefab at the grid point position and set the parent object to be the container object
                Vector3 instantPos = new Vector3(FindMaxAndMinInArray.xMin + col * spacing, yHeight, FindMaxAndMinInArray.zMin + row * spacing);
                GameObject _prefab = Instantiate(prefab, instantPos, Quaternion.identity);
                _prefab.transform.SetParent(container);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(instantKey))
        {
            InstantiateObj();
        }
    }
}
