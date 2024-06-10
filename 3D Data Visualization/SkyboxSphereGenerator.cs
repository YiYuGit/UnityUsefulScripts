using UnityEngine;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// This script read all the png files (panoramic image) from a folder and load them into new skybox materials.
/// Then for each of the skybox materials, make them into a spere, with certain space between them. 
/// This can work with a csv or txt file reader to read some location information to place the skybox photo spheres at correct location.
/// 
/// </summary>

public class SkyboxSphereGenerator : MonoBehaviour
{
    public string streamingAssetsPath = "Assets/StreamingAssets/PanoCapture";
    public GameObject spherePrefab;
    public float spacing = 5f;
    private List<Material> skyboxMaterials = new List<Material>();

    void Start()
    {
        LoadSkyboxMaterials();
        InstantiateSpheres();
    }

    
    void LoadSkyboxMaterials()
    {
        string[] imagePaths = Directory.GetFiles(streamingAssetsPath, "*.png");

        foreach (string imagePath in imagePaths)
        {
            // 

            byte[] fileData = File.ReadAllBytes(imagePath);
            //Texture2D texture = new Texture2D(2, 2);

            // For the thin seam problem. see here: https://discussions.unity.com/t/panoramic-skybox-has-white-seam-how-do-i-fix-that/205614/2
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGB24, false);

            texture.LoadImage(fileData);

            // test for fixing the seam problem
            //texture.anisoLevel = 0;
            //texture.filterMode = FilterMode.Bilinear;
            //texture.wrapMode = TextureWrapMode.Repeat;

            Material material = new Material(Shader.Find("Skybox/Panoramic"));
            material.SetTexture("_MainTex", texture);

            // Set the material name to the image file name without ".png"
            string imageName = Path.GetFileNameWithoutExtension(imagePath);
            material.name = imageName;

            // Add the material to the array or a list
            // You can later assign this array/list to the instantiated spheres
            // e.g., skyboxMaterials[index] = material;
            skyboxMaterials.Add(material); // Add the material to the list
        }
    }

    
    

    void InstantiateSpheres()
    {
        int index = 0;

        foreach (Material material in skyboxMaterials)
        {
            GameObject sphere = Instantiate(spherePrefab, new Vector3(index * spacing, 0, index * spacing), Quaternion.identity);
            sphere.name = material.name; 
            sphere.GetComponent<Renderer>().material = material;
            sphere.transform.parent = transform;

            index++;
        }
    }
}
