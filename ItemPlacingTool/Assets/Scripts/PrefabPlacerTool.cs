using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class PrefabPlacerTool : EditorWindow
{
    [MenuItem("Utilities/Prefabs")]
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI; 
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView obj) { 
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.MouseDown)
        {
            if (currentEvent.button == 0)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                Debug.Log("Left click happened. Ray from " + ray.origin + " to " + ray.direction);
            }
        } else if (currentEvent.type == EventType.KeyDown) {
            if (currentEvent.keyCode == KeyCode.A) {
                Debug.Log("A was pressed");
            }
        } else if (currentEvent.type == EventType.KeyUp) {
            if (currentEvent.keyCode == KeyCode.A) {
                Debug.Log("A was released");
            }
        }
    }
}
