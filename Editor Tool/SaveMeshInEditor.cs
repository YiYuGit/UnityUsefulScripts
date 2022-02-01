#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/// <summary>
/// This script is used to save the procedural mesh generated during gameplay as ".asset" so it can be used later.
/// 
/// </summary>

// Usage: Attach to gameobject, assign target gameobject (from where the mesh is taken), Run, Press savekey
// https://unitycoder.com/blog/2013/01/26/save-mesh-created-by-script-in-editor-playmode/

public class SaveMeshInEditor : MonoBehaviour
{

    public KeyCode saveKey = KeyCode.F12;
    public string saveName = "SavedMesh";
    public Transform selectedGameObject;

    void Update()
    {
        if (Input.GetKeyDown(saveKey))
        {
            SaveAsset();
        }
    }

    void SaveAsset()
    {
        var mf = selectedGameObject.GetComponent<MeshFilter>();
        if (mf)
        {
            var savePath = "Assets/" + saveName + ".asset";
            Debug.Log("Saved Mesh to:" + savePath);
            AssetDatabase.CreateAsset(mf.mesh, savePath);
        }
    }
}
#endif