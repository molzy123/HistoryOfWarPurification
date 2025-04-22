using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    
    public class BindingContextEditor
    {
        private static Dictionary<Type,string> exportType = new Dictionary<Type, string>()
        {
            {typeof(Image), "Image"},
            {typeof(TextMeshProUGUI), "TextMeshProUGUI"},
            {typeof(Button), "Button"},
            {typeof(Slider), "Slider"}
        };
        
        public static void bindingContext(GameObject go)
        {
            BindingContext prefabScript = go.GetComponent<BindingContext>();
            if (prefabScript == null)
            {
                return;
            }
            if (prefabScript.bindings == null) prefabScript.bindings = new List<GameObject>();
            prefabScript.bindings.Clear();
            List<GameObject> childObjects = new List<GameObject>();
            EditorUtil.GetAllChildren(go.transform, childObjects);
            // 遍历所有子物体，将名称以下划线开头的物体添加到列表中
            foreach (GameObject child in childObjects)
            {
                if (child.name.StartsWith("_"))
                {
                    prefabScript.bindings.Add(child);
                }
            }
            // generateScript(go, prefabScript.bindings);
            // 提示保存成功
            Debug.Log("保存成功！");  
        }

        private static void generateScript(GameObject go , List<GameObject> bindings)
        {
            string prefabName = go.name;
            string scriptName = "Go" + prefabName;
            string scriptPath = Application.dataPath + "/Scripts/generated/" + scriptName + ".cs";

            using (StreamWriter writer = new StreamWriter(scriptPath))
            {
                writer.WriteLine("using UnityEngine;");
                writer.WriteLine("using TMPro;");
                writer.WriteLine("using UnityEngine.UI;");
                writer.WriteLine();
                writer.WriteLine("public class " + scriptName);
                writer.WriteLine("{");
                foreach (var gameObject in bindings)
                {
                    string propertyName = gameObject.name;
                    writer.WriteLine(getFieldString(gameObject));
                }
                writer.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }

        private static string getFieldString(GameObject child)
        {
            string fieldName = child.name.Substring(1);
            Component[] components = child.GetComponents(typeof(Component));

            string fieldType = "GameObject";
            foreach (var component in components)
            {
                if (exportType.ContainsKey(component.GetType()))
                {
                    fieldType = exportType[component.GetType()];
                    break;
                }
            }
            
            string fieldString = "    public " + fieldType + " " + fieldName + ";";
            return fieldString;
        }
        
        
        
        
    }
}