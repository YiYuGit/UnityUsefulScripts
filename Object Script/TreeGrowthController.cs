using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script simulate the tree growth over time. 
/// The method used here is manipulate the scale of the model to represent the growth. 
/// The tree models are all placed under one empty object. This script find all the children of the empty object, 
/// and use a slide bar on the canvas to adjust the growth of all of them.  
/// As for the scale of x y z, the x and z are the same growing speed, and y is a different speed. Because tree usually grow higher than wider. 
/// These numbers should be adjustable.
/// Remember to set up the slider on in the inspector. The On Value Change, drop in the tree parent object and choose the fucntion of AdjustTreeGrowth()
/// </summary>

public class TreeGrowthController : MonoBehaviour
{
    public Slider growthSlider;
    public float xzGrowthSpeed = 0.1f;
    public float yGrowthSpeed = 0.2f;
    private Vector3 originalScale;

    private void Start()
    {
        // Store the original scale of the trees
        originalScale = transform.localScale;

        // Set the slider value to the left side (representing the original size)
        growthSlider.value = 0f;
        // Adjust tree growth based on the initial slider value
        AdjustTreeGrowth();
    }

    public void AdjustTreeGrowth()
    {
        // Get the growth factor from the slider (ranging from -1 to 1)
        float growthFactor = 2 * (growthSlider.value - 0.0f);

        // Iterate through all children of the empty GameObject
        foreach (Transform treeTransform in transform)
        {
            // Calculate the new scale based on growth factor and speed
            float xScale = originalScale.x + xzGrowthSpeed * growthFactor;
            float yScale = originalScale.y + yGrowthSpeed * growthFactor;
            float zScale = originalScale.z + xzGrowthSpeed * growthFactor;

            // Set the new scale for each tree
            treeTransform.localScale = new Vector3(xScale, yScale, zScale);
        }
    }
}
