//// ���: Assets/Editor/FindMissingScripts.cs
//using UnityEditor;
//using UnityEngine;

//public class FindMissingScripts : EditorWindow
//{
//    [MenuItem("Tools/Find Missing Scripts In Scene")]
//    public static void FindMissing()
//    {
//        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
//        int count = 0;

//        foreach (GameObject go in allObjects)
//        {
//            Component[] components = go.GetComponents<Component>();

//            for (int i = 0; i < components.Length; i++)
//            {
//                if (components[i] == null)
//                {
//                    Debug.LogWarning($"Missing script in GameObject: '{go.name}'", go);
//                    count++;
//                }
//            }
//        }

//        Debug.Log($"�� {count}���� ������Ʈ�� Missing Script�� �ֽ��ϴ�.");
//    }
//}
