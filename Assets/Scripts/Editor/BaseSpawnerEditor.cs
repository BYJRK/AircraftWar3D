using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseSpawner), true)]
public class BaseSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var t = target as BaseSpawner;

        if (t.floatingFactor > 0)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("Actual Interval");
            GUILayout.TextField($"{t.spawnInterval * (1 - t.floatingFactor):F2}");
            GUILayout.TextField($"{t.spawnInterval * (1 + t.floatingFactor):F2}");

            GUILayout.EndHorizontal();
        }
    }
}
