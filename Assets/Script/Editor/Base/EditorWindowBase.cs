using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class EditorWindowBase : EditorWindow
{
    protected Color selectedColor = Color.red;
    public virtual void Awake()
    {
        Undo.undoRedoPerformed += RepaintWindow;
    }

    void RepaintWindow()
    {
        Repaint();
    }







}
