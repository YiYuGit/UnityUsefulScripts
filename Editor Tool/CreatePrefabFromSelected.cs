using UnityEditor;
using UnityEngine;

/// <summary>
/// This is a menu script. This is from:
/// https://unitycoder.com/blog/2013/01/26/save-mesh-created-by-script-in-editor-playmode/
/// Creates a prefab from a selected game object. Can be used to save gameObject and mesh that are genenrated during gameplay into prefab and asset.
/// Adds a menu named "Create Prefab From Selected" to the GameObject menu.
/// This script can save the gameobject in play mode to a prefab and also make the mesh of this prefab into a mesh asset.
/// </summary>
class CreatePrefabFromSelected
{
    const string menuName = "GameObject/Create Prefab From Selected";

    [MenuItem(menuName)]
    static void CreatePrefabMenu()
    {
        var go = Selection.activeGameObject;

        Mesh m1 = go.GetComponent<MeshFilter>().mesh;

        var meshSavePath = "Assets/savedMesh/" + go.name + "_M" + ".asset";

        AssetDatabase.CreateAsset(m1, meshSavePath); 

        var savePath = "Assets/Prefabs/" + go.name + ".prefab";

        PrefabUtility.SaveAsPrefabAsset(go, savePath, out bool success);

    }

    // Validates the menu.
    // The item will be disabled if no game object is selected.
    // returns True if the menu item is valid.
    [MenuItem(menuName, true)]
    static bool ValidateCreatePrefabMenu()
    {
        return Selection.activeGameObject != null;
    }
}