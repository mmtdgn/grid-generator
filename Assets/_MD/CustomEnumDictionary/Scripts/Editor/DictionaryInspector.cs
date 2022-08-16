/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
*/
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
//ReorderableList
namespace MD.EnumDictionary
{
    [CustomEditor(typeof(TileDictionary))]
    //[CustomEditor(typeof(YourOwnDictionaryClass))]
    public class DictionaryInspector : Editor
    {
        private bool m_ShowListSpace;
        private SerializedProperty m_Property;
        private ReorderableList m_List;

        private void OnEnable()
        {
            m_Property = serializedObject.FindProperty("_T1");
            m_List = new ReorderableList(serializedObject, m_Property, true, true, false, false)
            {
                drawHeaderCallback = DrawListHeader,
                drawElementCallback = DrawListElement
            };
        }

        private void DrawListHeader(Rect rect)
        {
            GUI.Label(rect, "GridTile");
        }

        private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var item = m_Property.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, item);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);

            LayoutToggle();
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private void LayoutToggle()
        {
            if (m_ShowListSpace)
            {
                if (GUILayout.Button("Close Edit Panel"))
                    m_ShowListSpace = false;
                m_List.DoLayoutList();
                ReorderList();
            }
            else
            {
                if (GUILayout.Button("Edit Dictionary"))
                    m_ShowListSpace = true;
            }
        }

        public void ReorderList()
        {
            TileDictionary _base = target as TileDictionary;
            if (GUILayout.Button("Init & Reset", GUILayout.Height(30)))
            {
                if (EditorUtility.DisplayDialog("Reload Enum Dictionary", "Are you sure you want to reset enum dictionary?\n\n All data will be gone!", "Yes", "No"))
                {
                    _base.ReOrder();
                }
            }
        }
    }
}
