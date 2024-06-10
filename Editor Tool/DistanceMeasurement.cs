using UnityEngine;

/// <summary>
/// This script is a editor tool to measure the distance between two objects in the scene. 
/// (To work in editor mode, it also need DistanceMeasurementEditor.cs script in the Editor folder under the Assets)
/// 
/// </summary>


public class DistanceMeasurement : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    public void MeasureDistance()
    {
        if (object1 != null && object2 != null)
        {
            float distance = Vector3.Distance(object1.position, object2.position);
            Debug.Log("Distance between " + object1.name + " and " + object2.name + ": " + distance.ToString("F2") + " units");
        }
        else
        {
            Debug.LogError("Please assign both objects before measuring distance.");
        }
    }
}
