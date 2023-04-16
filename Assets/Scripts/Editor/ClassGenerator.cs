#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEditor.Compilation;

public class AddComponentToNewPrefabEditor : EditorWindow
{
    private string prefabName = "";
    private string componentName = "";
    private GameObject prefab;
    private DefaultAsset _directoryAsset;

    [MenuItem("Window/Add Component to New Prefab")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AddComponentToNewPrefabEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("クラスジェネレーター", EditorStyles.boldLabel);

        if (EditorApplication.isCompiling)
        {
            Color bg = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            EditorGUILayout.HelpBox("コンパイル中", MessageType.Info);
            GUI.backgroundColor = bg;
            base.Repaint();
            return;
        }
        prefabName = EditorGUILayout.TextField("プレハブの名前", prefabName);
        componentName = EditorGUILayout.TextField("コンポーネントの名前", componentName);
        _directoryAsset = (DefaultAsset)EditorGUILayout.ObjectField("生成先のディレクトリを指定", _directoryAsset, typeof(DefaultAsset), true);
        string path = AssetDatabase.GetAssetPath(_directoryAsset);
        if (GUILayout.Button("クラスを生成"))
        {
            Debug.Log(path);
            string componentPath = path + "/" + componentName + ".cs";
            if (AssetDatabase.LoadAssetAtPath(componentPath, typeof(MonoScript)))
            {
                Debug.LogWarning("コンポーネントが既に存在します " + prefabName);
                return;
            }
            File.WriteAllText(componentPath, "using UnityEngine;\r\n\r\npublic class " + componentName + " : MonoBehaviour\r\n{\r\n    // Start is called before the first frame update\r\n    void Start()\r\n    {\r\n        \r\n    }\r\n\r\n    // Update is called once per frame\r\n    void Update()\r\n    {\r\n        \r\n    }\r\n}");
            CompilationPipeline.RequestScriptCompilation();
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("プレハブ生成"))
        {
            string prefabPath = path + "/" + prefabName + ".prefab";
            if (AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)))
            {
                Debug.LogWarning("プレハブが既に存在します " + componentName);
                return;
            }
            // プレハブを作成
            prefab = new GameObject(prefabName);

            // プレハブにコンポーネントを追加する
            Type componentType = Type.GetType(componentName + ", Assembly-CSharp"); ;
            if (componentType == null)
            {
                Debug.LogError("コンポーネントが見つかりませんでした: " + componentName);
                return;
            }

            Component component = prefab.AddComponent(componentType);
            Debug.Log(component);
            if (component == null)
            {
                Debug.LogError("コンポーネントがアタッチできませんでした: " + componentName);
            }
            // プレハブを保存する           
            PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
            DestroyImmediate(prefab);
        }
    }
}
#endif