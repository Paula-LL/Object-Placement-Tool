using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PrefabPlace : Editor
{
    GameObject lastInstantiateObject = null;

    void CreateEmptyGameObjectInEditor() {
        lastInstantiateObject = new GameObject("PersistentGameObject");
        Undo.RegisterCreatedObjectUndo(lastInstantiateObject, "CreateEmptyGameObjectInEditor");
    }

    void DestroyLastPesistentGameObjectFromEditor() { 
        if(lastInstantiateObject != null)
        {
            Undo.DestroyObjectImmediate(lastInstantiateObject);
        }
    }

    void ChangeLastPersistentGameObjectName() { 
        if (lastInstantiateObject != null)
        {
            Undo.RecordObject(lastInstantiateObject, "ChangeLastPersistentGameObjectName");
            lastInstantiateObject.name += "Altered";
        }
    }
}