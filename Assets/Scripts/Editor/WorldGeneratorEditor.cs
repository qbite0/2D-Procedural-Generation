using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(WorldGenerator))]
public class WorldGeneratorEditor : Editor
{
    WorldGenerator wg;

    private BoxBoundsHandle AreaHandle = new BoxBoundsHandle();

    private void OnEnable()
    {
        wg = target as WorldGenerator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Stop Generate"))
        {
            wg.StopGenerate();
        }
        EditorGUILayout.HelpBox("WorldGenerator - Use for procedural generation of plants and buildings. \n Made by qBite with ♥ \n Ver: 0.1.2", MessageType.Info);
    }

    private void OnSceneGUI()
    {
        AreaHandle.center = wg.Area.center;
        AreaHandle.size = wg.Area.size;

        EditorGUI.BeginChangeCheck();

        AreaHandle.DrawHandle();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(wg, "Change Area");

            Bounds newBounds = new Bounds();
            newBounds.center = AreaHandle.center;
            newBounds.size = AreaHandle.size;
            wg.Area = newBounds;
        }

        if (wg.Draw)
        {
            switch (wg.Type)
            {
                case WorldGenerator.GenerationType.Circle:
                    DrawCircles();
                    break;
                case WorldGenerator.GenerationType.Box:
                    DrawBoxes();
                    break;
            }
        }
    }

    private void DrawBoxes()
    {
        Vector2 Current = (Vector2)wg.Area.min + (wg.BoxSize / 2);

        while (Current.y < wg.Area.max.y)
        {
            while (Current.x < wg.Area.max.x)
            {
                Handles.DrawWireCube(Current, wg.BoxSize);
                Current.x += wg.BoxSize.x + wg.Distance;
            }
            Current.x = wg.Area.min.x + (wg.BoxSize.x / 2);
            Current.y += wg.BoxSize.y + wg.Distance;
        }
    }

    private void DrawCircles()
    {
        Vector2 Current = (Vector2)wg.Area.min + (Vector2.one * wg.CircleRadius);

        while (Current.y < wg.Area.max.y)
        {
            while (Current.x < wg.Area.max.x)
            {
                Handles.DrawWireDisc(Current, Vector3.back, wg.CircleRadius);
                Current.x += (wg.CircleRadius * 2) + wg.Distance;
            }
            Current.x = wg.Area.min.x + wg.CircleRadius;
            Current.y += (wg.CircleRadius * 2) + wg.Distance;
        }
    }
}
