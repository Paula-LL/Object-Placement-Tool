using UnityEditor;
using UnityEngine;

public class PrefabPlacerTool : EditorWindow
{
    GameObject activePrefab;
    Transform prefabTransform;
    GameObject lastInstance;


    [MenuItem("Tools/Prefab Placer Tools")]
    public static void ShowWindow()
    {

        EditorWindow.GetWindow(typeof(PrefabPlacerTool), false, "Prefab Placer Tool");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        Event currentEvent = Event.current;

        GUILayout.Label("Prefab Placer Tool");

        activePrefab = (GameObject)EditorGUILayout.ObjectField("ACtive Prefab", activePrefab, typeof(GameObject), false);
        prefabTransform = (Transform)EditorGUILayout.ObjectField("Parent Object", prefabTransform, typeof(Transform), true);
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event currentEvent = Event.current;

        if (activePrefab == null)
        {
            return;
        }

        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                CreatePrefabInstance(hit.point);
                currentEvent.Use();
            }
        }

        if (currentEvent.type == EventType.MouseDrag && lastInstance != null)
        {
            float rotationSpeed = 0.5f;
            Undo.RecordObject(lastInstance.transform, "Rotate Prefab Instance");

            lastInstance.transform.Rotate(Vector3.up, currentEvent.delta.x * rotationSpeed, Space.World);
            currentEvent.Use();
        }
    }

    private void CreatePrefabInstance(Vector3 position)
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(activePrefab);

        Undo.RegisterCreatedObjectUndo(instance, "Instantiate Prefab");

        instance.transform.position = position;

        if (prefabTransform != null)
        {
            instance.transform.SetParent(prefabTransform);
        }

        lastInstance = instance;
    }
}
