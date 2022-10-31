using UnityEditor;
using UnityEngine.SceneManagement;

public class SortPhysicsObject
{
    [MenuItem("Tool/Set PhysicsObject ID")]
    static void Sort()
    {
        var id = 1;
        var gos = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var go in gos)
        {
            var pos = go.GetComponentsInChildren<PhysicsObject>();
            foreach (var p in pos)
            {
                if (p)
                {
                    p.id = id;
                    id++;
                }
            }
        }
        AssetDatabase.SaveAssets();
    }
}
