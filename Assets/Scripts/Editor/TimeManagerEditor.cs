// thanks to https://blog.terresquall.com/2020/03/creating-reorderable-lists-in-the-unity-inspector/ for the base of this script.

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Mechanics
{ 
    [CustomEditor(typeof(TimeManager))]
    public class TimeManagerEditor : Editor
    {
        //The array property we will edit
        SerializedProperty timePoints;
        SerializedProperty progressDurationSeconds;
        SerializedProperty maxTimePerRoomVists;
        SerializedProperty activeTimePointIndex;
        SerializedProperty progressTimePerRoomVisit;

        //The Reorderable list we will be working with
        ReorderableList timePointsList;
        ReorderableList maxTimePointsPerRoomList;

        Vector2 currentElementPos = default(Vector2);

        private void OnEnable()
        {
            timePoints = serializedObject.FindProperty("m_timePoints");
            progressDurationSeconds = serializedObject.FindProperty("m_progressDurationSeconds");
            maxTimePerRoomVists = serializedObject.FindProperty("m_maxTimePerRoomVisits");
            activeTimePointIndex = serializedObject.FindProperty("m_activeTimePointIndex");
            progressTimePerRoomVisit = serializedObject.FindProperty("m_progressTimePerRoomVisit");

            timePointsList = new ReorderableList(serializedObject, timePoints, false, true, true, true);
            maxTimePointsPerRoomList = new ReorderableList(serializedObject, maxTimePerRoomVists, false, true, true, true);

            timePointsList.drawElementCallback = DrawTimePointsListItem;
            timePointsList.drawHeaderCallback = DrawTimePointsListHeader;

            maxTimePointsPerRoomList.drawElementCallback = DrawMaxTimePerRoomListItem;
            maxTimePointsPerRoomList.drawHeaderCallback = DrawMaxTimePerVisitListHeader;

        }

        void DrawTimePointsListItem(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = timePointsList.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

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

            EditorGUI.LabelField(new Rect(currentElementPos.x, currentElementPos.y, 60, EditorGUIUtility.singleLineHeight), "effects");

            currentElementPos.x += 50;

            EditorGUI.PropertyField(
                new Rect(currentElementPos.x, currentElementPos.y, 20, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("timeEffect"),
                GUIContent.none
            );

            currentElementPos.x += 30;

            EditorGUI.LabelField(new Rect(currentElementPos.x, currentElementPos.y, 100, EditorGUIUtility.singleLineHeight), "time");

            currentElementPos.x += 30;

            EditorGUI.PropertyField(
                new Rect(currentElementPos.x, currentElementPos.y, 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("activationTime"),
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

        void DrawMaxTimePerRoomListItem(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = maxTimePointsPerRoomList.serializedProperty.GetArrayElementAtIndex(index); //The element in the list

            // Create a property field and label field for each property. 

            currentElementPos.x = rect.x;
            currentElementPos.y = rect.y;

            EditorGUI.LabelField(new Rect(currentElementPos.x, currentElementPos.y, 100, EditorGUIUtility.singleLineHeight), index + " Vists");

            currentElementPos.x += 60;

            EditorGUI.PropertyField(
                new Rect(currentElementPos.x, currentElementPos.y, 100, EditorGUIUtility.singleLineHeight),
                element,
                GUIContent.none
            );
        }

        void DrawTimePointsListHeader(Rect rect)
        {
            string name = "Time Point (seconds)";
            EditorGUI.LabelField(rect, name);
        }

        void DrawMaxTimePerVisitListHeader(Rect rect)
        {
            string name = "Max Time Per Room Vists (seconds)";
            EditorGUI.LabelField(rect, name);
        }

        //This is the function that makes the custom editor work
        public override void OnInspectorGUI()
        {
            if ((Event.current.type == EventType.MouseDown && Event.current.button == 0) ||
                (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) ||
                (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape))
            {
                (target as TimeManager).OrderTimePoints();
            }

            serializedObject.Update();

            EditorGUILayout.PropertyField(progressTimePerRoomVisit);

            maxTimePointsPerRoomList.DoLayoutList();
            timePointsList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}