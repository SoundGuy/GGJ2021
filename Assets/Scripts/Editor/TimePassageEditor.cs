// thanks to https://blog.terresquall.com/2020/03/creating-reorderable-lists-in-the-unity-inspector/ for the base of this script.

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Mechanics
{ 
    [CustomEditor(typeof(TimePassage))]
    public class TimePassageEditor : Editor
    {
        //The array property we will edit
        SerializedProperty timePoints;
        SerializedProperty progressDurationSeconds;
        SerializedProperty activeTimePointIndex;

        //The Reorderable list we will be working with
        ReorderableList list;

        Vector2 currentElementPos = default(Vector2);

        private void OnEnable()
        {
            timePoints = serializedObject.FindProperty("m_timePoints");
            progressDurationSeconds = serializedObject.FindProperty("m_progressDurationSeconds");
            activeTimePointIndex = serializedObject.FindProperty("m_activeTimePointIndex");

            list = new ReorderableList(serializedObject, timePoints, true, true, true, true);

            list.drawElementCallback = DrawListItems;
            list.drawHeaderCallback = DrawHeader;
        }

        void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

            // Create a property field and label field for each property. 

            currentElementPos.x = rect.x;
            currentElementPos.y = rect.y;

            EditorGUI.LabelField(new Rect(currentElementPos.x, currentElementPos.y, 100, EditorGUIUtility.singleLineHeight), "label");

            currentElementPos.x += 30;

            EditorGUI.PropertyField(
                new Rect(currentElementPos.x, currentElementPos.y, 100, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("label"),
                GUIContent.none
            );

            currentElementPos.x += 120;

            EditorGUI.LabelField(new Rect(currentElementPos.x, currentElementPos.y, 100, EditorGUIUtility.singleLineHeight), "timeEffect");

            currentElementPos.x += 60;

            EditorGUI.PropertyField(
                new Rect(currentElementPos.x, currentElementPos.y, 20, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("timeEffect"),
                GUIContent.none
            );

            // Active Time Point Notifier
            if (activeTimePointIndex.intValue == index)
            {
                EditorGUI.DrawRect(new Rect(currentElementPos.x + 40, currentElementPos.y, 10, EditorGUIUtility.singleLineHeight), Color.green);
            }
            else
            {
                EditorGUI.DrawRect(new Rect(currentElementPos.x + 40, currentElementPos.y, 10, EditorGUIUtility.singleLineHeight), Color.grey);
            }
            
        }

        void DrawHeader(Rect rect)
        {
            string name = "Time Point";
            EditorGUI.LabelField(rect, name);
        }

        //This is the function that makes the custom editor work
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(progressDurationSeconds);

            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}