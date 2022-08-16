/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
*/
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TileSettings))]
public class TileSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var labelRect = new Rect(80, position.y, 70, position.height);
        var coordinate = new Rect(160, position.y, 80, position.height);
        var endField = new Rect(position.x + 80, position.y, position.width - 80, position.height);

        property.FindPropertyRelative("name").stringValue = " ";
        EditorGUI.LabelField(labelRect, "Coordinate");
        EditorGUI.PropertyField(coordinate, property.FindPropertyRelative("Coordinate"), GUIContent.none);
        EditorGUI.PropertyField(endField, property.FindPropertyRelative("TileType"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}