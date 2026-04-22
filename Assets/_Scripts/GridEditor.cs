using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ItemSpawner))]
public class GridEditor : Editor
{
    private ItemSpawner spawner;
    private void Awake()
    {
        spawner = (ItemSpawner) target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("CreateGrid"))
        {
            spawner.StartFillItemToBoard();
        }        
        if (GUILayout.Button("ClearGrid"))
        {
            spawner.ClearFullItem();
        }
    }
}
