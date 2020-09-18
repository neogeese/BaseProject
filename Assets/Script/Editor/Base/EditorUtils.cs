using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class EditorUtils
{

    /// <summary>
    /// 기본 적인 ReorderableList를 생성 한다. Inspector나 독립적인 window에서 사용 가능. 
    /// </summary>
    /// <typeparam name="T">List 에서 사용할 Type</typeparam>
    /// <param name="headerText">리스트 제목</param>
    /// <param name="elements">리스트를 구성할 배열</param>
    /// <param name="currentElement">현재 선택된 elment</param>
    /// <param name="drawElement">Draw CallBack</param>
    /// <param name="selectElement">Select Calback</param>
    /// <param name="createElement">Create Callback</param>
    /// <param name="removeElement">Remove Callback</param>
    /// <returns></returns>
    public static ReorderableList SetupReorderableList<T>(string headerText, List<T> elements, ref T currentElement, Action<Rect, T, int> drawElement, Action<T> selectElement, Action createElement, Action<T, int> removeElement)
    {
        var list = new ReorderableList(elements, typeof(T), true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, headerText);
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = elements[index];
            drawElement(rect, element, index);
        };

        list.onSelectCallback = (ReorderableList l) =>
        {
            var selectedElement = elements[list.index];
            selectElement(selectedElement);
        };

        if (createElement != null)
        {
            list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) =>
            {
                createElement();
            };
        }

        list.onRemoveCallback = (ReorderableList l) =>
        {
            if (EditorUtility.DisplayDialog("Warning!", "delete this item??", "Yes", "No"))
            {
                var element = elements[l.index];
                removeElement(element, l.index);
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
            }
        };

        return list;
    }

    /// <summary>
    /// 편집만 가능한 ReorderableList 생성. 사용자에 의해 아이템이 추가 되거나 제거를 막아야 되는 경우 활용.
    /// </summary>
    /// <typeparam name="T">List 에서 사용할 Type</typeparam>
    /// <param name="headerText">리스트 제목</param>
    /// <param name="elements">리스트를 구성할 배열</param>
    /// <param name="currentElement">현재 선택된 elment</param>
    /// <param name="drawElement">Draw CallBack</param>
    /// <param name="selectElement">Select Calback</param>
    /// <returns></returns>
    public static ReorderableList SetupReorderableListReadOnly<T>(string headerText, List<T> elements, ref T currentElement, Action<Rect, T, int> drawElement, Action<T> selectElement)
    {
        var list = new ReorderableList(elements, typeof(T), false, true, false, false);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, headerText);
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = elements[index];
            drawElement(rect, element, index);
        };

        list.onSelectCallback = (ReorderableList l) =>
        {
            var selectedElement = elements[list.index];
            selectElement(selectedElement);
        };

        return list;
    }

    public static bool DrawSprite(Rect rect, Sprite sprite, bool drawBox = true)
    {
        if (drawBox) GUI.Box(rect, "");

        if (sprite != null)
        {
            Texture t = sprite.texture;
            Rect tr = sprite.textureRect;
            Rect r = new Rect(tr.x / t.width, tr.y / t.height, tr.width / t.width, tr.height / t.height);

            rect.x += 2;
            rect.y += 2;
            rect.width -= 4;
            rect.height -= 4;
            GUI.DrawTextureWithTexCoords(rect, t, r);
        }

        return false;
    }    


}
