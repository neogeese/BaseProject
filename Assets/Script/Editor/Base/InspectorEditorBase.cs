using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InspectorEditorBase : Editor
{

    protected static bool styleDefined = false;
    protected static GUIStyle headerStyle;
    protected static GUIStyle foldoutStyle;
    protected static GUIStyle conflictStyle;
    protected static GUIStyle toggleHeaderStyle;


    public override void OnInspectorGUI()
    {
        DefineStyle();
    }


    protected static void DefineStyle()
    {
        if (styleDefined) return;
        styleDefined = true;

        headerStyle = new GUIStyle("Label")
        {
            fontStyle = FontStyle.Bold
        };
        headerStyle.normal.textColor = Color.black;

        toggleHeaderStyle = new GUIStyle("Toggle")
        {
            fontStyle = FontStyle.Bold
        };
        toggleHeaderStyle.normal.textColor = Color.black;

        foldoutStyle = new GUIStyle("foldout")
        {
            fontStyle = FontStyle.Bold
        };
        foldoutStyle.normal.textColor = Color.black;

        conflictStyle = new GUIStyle("Label");
        conflictStyle.normal.textColor = Color.red;
    }

    protected bool showDefaultEditor = false;
    protected void DefaultInspector()
    {
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(10));
        showDefaultEditor = EditorGUILayout.Foldout(showDefaultEditor, "Show default editor", foldoutStyle);
        EditorGUILayout.EndHorizontal();
        if (showDefaultEditor) DrawDefaultInspector();

        EditorGUILayout.Space();
    }


    protected void DrawDefault(string name)
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(serializedObject.FindProperty(name));

        if (EditorGUI.EndChangeCheck() == true)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected bool BeginFoldout(ref bool flag, string title)
    {
        flag = EditorGUILayout.Foldout(flag, title, foldoutStyle);
        if (flag)
            EditorGUI.indentLevel++;
        return flag;
    }

    protected void EndFoldout()
    {
        EditorGUI.indentLevel--;
    }
}
