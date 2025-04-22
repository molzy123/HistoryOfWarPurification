using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ComponentCopyTool : EditorWindow
    {
        private GameObject sourcePrefab;
        private GameObject targetObject;

        [MenuItem("Tools/Component Copy Tool")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ComponentCopyTool));
        }

        private void OnGUI()
        {
            GUILayout.Label("Component Copy Tool", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            sourcePrefab = EditorGUILayout.ObjectField("Source Prefab", sourcePrefab, typeof(GameObject), true) as GameObject;
            targetObject = EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true) as GameObject;

            EditorGUILayout.Space();

            if (GUILayout.Button("Copy Components"))
            {
                if (sourcePrefab != null && targetObject != null)
                {
                    CopyComponents(sourcePrefab, targetObject);
                    Debug.Log("Components copied successfully!");
                }
                else
                {
                    Debug.LogError("Please assign source prefab and target object!");
                }
            }
        }

        private void CopyComponents(GameObject source, GameObject target)
        {
            // 获取源预制体上的所有组件
            Component[] components = source.GetComponents<Component>();

            foreach (Component component in components)
            {
                // 检查目标对象是否已经有相同类型的组件
                Component newComponent = target.GetComponent(component.GetType());

                if (newComponent == null)
                {
                    // 如果不存在相同类型的组件，则创建一个新的组件实例
                    newComponent = target.AddComponent(component.GetType());
                }

                // 将源组件的属性值复制到新组件上
                EditorUtility.CopySerialized(component, newComponent);
            }
        }



    }
}