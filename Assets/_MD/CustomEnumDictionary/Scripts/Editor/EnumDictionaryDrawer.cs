/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
*/
using UnityEngine;
using UnityEditor;
using MD.EnumDictionary;

[CustomPropertyDrawer(typeof(EnumDictionaryBase<TileType, GameObject, Color>))]
//Add new attribute what you need it!
//Example => [CustomPropertyDrawer(typeof(EnumDictionaryBase<TileType, string, int>))]
//           [CustomPropertyDrawer(typeof(EnumDictionaryBase<TileType, Animation, Transform>))]
public class EnumDictionaryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.backgroundColor = new Color(1f, 0.8f, 0.7f);
        GUI.contentColor = new Color(0.82f, 0.73f, 0.60f);

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var RectLeft = new Rect(40, position.y, 100, position.height);
        var RectMiddle = new Rect(150, position.y, 100, position.height);
        var RectRight = new Rect(260, position.y, position.max.x - 260, position.height);


        if (property.FindPropertyRelative("IsEnumFieldEditable").boolValue)//For label field
        {
            SerializedProperty keyValue = property.FindPropertyRelative("key");
            EditorGUI.LabelField(RectLeft, keyValue.enumDisplayNames[keyValue.enumValueIndex], EditorStyles.objectFieldThumb);
        }
        else//For enum field;
        {
            EditorGUI.PropertyField(RectLeft,
            property.FindPropertyRelative("key"), GUIContent.none);
        }

        SerializedProperty val1 = property.FindPropertyRelative("val1");
        EditorGUI.PropertyField(RectMiddle, val1, GUIContent.none);

        SerializedProperty val2 = property.FindPropertyRelative("val2");
        EditorGUI.PropertyField(RectRight, val2, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}