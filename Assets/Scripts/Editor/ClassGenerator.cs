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
        GUILayout.Label("�N���X�W�F�l���[�^�[", EditorStyles.boldLabel);

        if (EditorApplication.isCompiling)
        {
            Color bg = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            EditorGUILayout.HelpBox("�R���p�C����", MessageType.Info);
            GUI.backgroundColor = bg;
            base.Repaint();
            return;
        }
        prefabName = EditorGUILayout.TextField("�v���n�u�̖��O", prefabName);
        componentName = EditorGUILayout.TextField("�R���|�[�l���g�̖��O", componentName);
        _directoryAsset = (DefaultAsset)EditorGUILayout.ObjectField("������̃f�B���N�g�����w��", _directoryAsset, typeof(DefaultAsset), true);
        string path = AssetDatabase.GetAssetPath(_directoryAsset);
        if (GUILayout.Button("�N���X�𐶐�"))
        {
            Debug.Log(path);
            string componentPath = path + "/" + componentName + ".cs";
            if (AssetDatabase.LoadAssetAtPath(componentPath, typeof(MonoScript)))
            {
                Debug.LogWarning("�R���|�[�l���g�����ɑ��݂��܂� " + prefabName);
                return;
            }
            File.WriteAllText(componentPath, "using UnityEngine;\r\n\r\npublic class " + componentName + " : MonoBehaviour\r\n{\r\n    // Start is called before the first frame update\r\n    void Start()\r\n    {\r\n        \r\n    }\r\n\r\n    // Update is called once per frame\r\n    void Update()\r\n    {\r\n        \r\n    }\r\n}");
            CompilationPipeline.RequestScriptCompilation();
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("�v���n�u����"))
        {
            string prefabPath = path + "/" + prefabName + ".prefab";
            if (AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)))
            {
                Debug.LogWarning("�v���n�u�����ɑ��݂��܂� " + componentName);
                return;
            }
            // �v���n�u���쐬
            prefab = new GameObject(prefabName);

            // �v���n�u�ɃR���|�[�l���g��ǉ�����
            Type componentType = Type.GetType(componentName + ", Assembly-CSharp"); ;
            if (componentType == null)
            {
                Debug.LogError("�R���|�[�l���g��������܂���ł���: " + componentName);
                return;
            }

            Component component = prefab.AddComponent(componentType);
            Debug.Log(component);
            if (component == null)
            {
                Debug.LogError("�R���|�[�l���g���A�^�b�`�ł��܂���ł���: " + componentName);
            }
            // �v���n�u��ۑ�����           
            PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
            DestroyImmediate(prefab);
        }
    }
}
#endif