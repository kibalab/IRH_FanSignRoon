using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IRH_Panel_Macher : EditorWindow
{
    public GameObject[] Panel;
    public RenderTexture[] textures;
    public Material[] materials;

    [MenuItem("Iroha/Panel_Matcher")]
    public static void init()
    {
        var window = EditorWindow.CreateWindow<IRH_Panel_Macher>();
        window.Show();
    }

    private void OnGUI()
    {
        ScriptableObject scriptableObj = this;
        SerializedObject serialObj = new SerializedObject(scriptableObj);
        SerializedProperty serialPanel = serialObj.FindProperty("Panel");
        SerializedProperty serialTex = serialObj.FindProperty("textures");
        SerializedProperty serialMat = serialObj.FindProperty("materials");

        EditorGUILayout.PropertyField(serialPanel, true);
        EditorGUILayout.PropertyField(serialTex, true);
        EditorGUILayout.PropertyField(serialMat, true);
        

        serialObj.ApplyModifiedProperties();



        if (GUILayout.Button("Apply"))
        {
            for (var i = 0; i < Panel.Length; i++)
            {
                Debug.Log($"Apply {i}");
                var obj = Panel[i];
                var renderer = obj.transform.GetChild(0).GetChild(1);
                var cam = obj.transform.GetChild(0).GetChild(2);
                var clearCam = obj.transform.GetChild(0).GetChild(3);

                cam.GetComponent<Camera>().targetTexture = textures[i];
                clearCam.GetComponent<Camera>().targetTexture = textures[i];

                materials[i].SetTexture("_MainTex", textures[i]);
                renderer.GetComponent<MeshRenderer>().material = materials[i];
            }
            Debug.Log("Apply Success");
        }
    }
}
