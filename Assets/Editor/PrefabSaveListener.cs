using System.Collections.Generic;
using Editor;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class PrefabSaveListener
{
    [InitializeOnLoadMethod]
    static void RegisterPrefabStageEvents()
    {
        PrefabStage.prefabSaving += OnSaving;
        PrefabStage.prefabSaved +=OnSaved;
        PrefabStage.prefabStageClosing += OnClosing;
        PrefabStage.prefabStageOpened += OnOpend;
    }

    static void OnSaving(GameObject go)
    {
        Debug.LogFormat("GameObject({0}) is saving.", go.name);
    }

    static void OnSaved(GameObject go)
    {
        BindingContextEditor.bindingContext(go);
    }

    static void OnOpend(PrefabStage stage)
    {
        Debug.LogFormat("GameObject({0}) is opend.", stage.assetPath);
    }

    static void OnClosing(PrefabStage stage)
    {
        Debug.LogFormat("GameObject({0}) is closing.", stage.assetPath);
    }
}