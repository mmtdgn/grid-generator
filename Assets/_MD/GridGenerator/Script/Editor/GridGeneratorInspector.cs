/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
*/
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorInspector : Editor
{
    private int m_X, m_Y;
    private GridGenerator m_Target;

    private void OnEnable()
    {
        m_Target = (GridGenerator)target;
        m_Target.UpdateGridSizeField();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(EditorStyles.objectFieldThumb);
        DrawLabel();
        DrawReferenceField();
        DrawSettings();
        DrawGrid();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawReferenceField()
    {
        BeginOutBorder();
        EditorGUI.BeginChangeCheck();

        BeginInnerBorder();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_GridData"), true);
        EndInnerBorder();


        SerializedProperty _GridRoot = serializedObject.FindProperty("m_Grid");
        if (m_Target.Grid != null)
        {
            BeginInnerBorder();
            EditorGUILayout.PropertyField(_GridRoot, true);
            EndInnerBorder();
        }
        if (EditorGUI.EndChangeCheck())
        {
            OnAnyValueChanged();
        }
        EndOutBorder();
    }

    private void OnAnyValueChanged()
    {
        m_Target.UpdateGridSizeField();
    }

    private void DrawLabel()
    {
        GUI.contentColor = Color.cyan;
        GUIStyle _style = new GUIStyle()
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 20,
            normal =
            {
                textColor = Color.cyan
            }
        };

        GUI.backgroundColor = Color.gray;

        GUILayout.BeginVertical(EditorStyles.objectFieldThumb);
        Space(5f);

        GUILayout.Label("Grid Generator", _style);

        Space(5f);
        GUILayout.EndVertical();
        GUI.contentColor = Color.white;
    }
    private void DrawSettings()
    {
        GUI.backgroundColor = Color.gray;

        BeginOutBorder();
        BeginInnerBorder();


        if (!m_Target.IsGridDataNull)
        {
            m_Target.GetGridData();
            m_X = m_Target.GridData.GridSize.x;
            m_Y = m_Target.GridData.GridSize.y;
        }

        GUILayout.Label("Grid size", GUILayout.Width(100f));

        GUILayout.Label("X", GUILayout.Width(11f));
        m_Target.X = EditorGUILayout.IntField(serializedObject.FindProperty("m_X").intValue, GUILayout.MinWidth(30f));
        GUILayout.Label("Y", GUILayout.Width(11f));
        m_Target.Y = EditorGUILayout.IntField(serializedObject.FindProperty("m_Y").intValue, GUILayout.MinWidth(30f));

        if (GUILayout.Button("Generate"))
        {
            if (m_X > 0 && m_Y > 0)
            {
                if (EditorUtility.DisplayDialog("Generate Grid Layout", "Are you sure you want to Regenerate Grid?\n\n Existing data will be gone!", "Yes", "No"))
                {
                    GenerateGridLayout();
                }
            }
            else
            {
                GenerateGridLayout();
            }
        }

        GUI.backgroundColor = Color.gray;

        EndInnerBorder();
        EndOutBorder();
    }
    private void GenerateGridLayout()
    {
        m_X = m_Target.X;
        m_Y = m_Target.Y;
        m_Target.SetGridSize(m_X, m_Y);
        m_Target.UpdateGrid(m_X * m_Y);
    }
    private void DrawGrid()
    {
        BeginOutBorder(15f);
        GUILayout.BeginHorizontal();
        Space(10);

        GUI.backgroundColor = new Color(1f, 1f, 1f);
        GUILayout.BeginVertical(EditorStyles.objectFieldThumb);
        GUI.backgroundColor = Color.gray;
        Space(5);
        for (var i = m_Y - 1; i >= 0; i--)
        {
            GUILayout.BeginHorizontal();
            Space(5);
            for (var j = 0; j < m_X; j++)
            {

                GUI.backgroundColor = m_Target.GetColor(j, i);
                if (GUILayout.Button($"{j},{i}\n{m_Target.GetTypeName(j, i)} ", EditorStyles.objectField, GUILayout.MinHeight(40f)))
                {
                    m_Target.ChangeState(j, i);
                }
                // if (j != _x - 1)
                Space(5);

                GUI.backgroundColor = Color.gray;
            }
            GUILayout.EndHorizontal();

            // if (i != 0)
            Space(5);
        }
        GUILayout.EndVertical();
        Space(10);
        GUILayout.EndHorizontal();
        Space(15);
        DrawCreateButtonField();
        GUILayout.EndVertical();
    }
    private void DrawCreateButtonField()
    {

        if (m_X > 0 && m_Y > 0)
        {
            if (m_Target.Grid == null)
            {
                GUI.backgroundColor = Color.gray;
                GUI.contentColor = Color.white;
            }
            else
            {
                GUI.backgroundColor = Color.white;
                GUI.contentColor = Color.black;
            }
            BeginInnerBorder();
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("Create Grid", GUILayout.Height(25f)))
            {
                CreateGrid();
            }

            if (GUILayout.Button("Create Grid as Prefab", GUILayout.Height(25f)))
            {
                CreateGrid(true);
            }
            EditorGUILayout.EndVertical();
            EndInnerBorder();
            Space(10);
        }
    }

    private void CreateGrid(bool asPrefab = false)
    {
        m_Target.UpdateTileColors();
        m_Target.CreateGrid(asPrefab);
    }

    #region OutBorder
    private void BeginOutBorder(float pixel = 10f)
    {
        GUILayout.BeginVertical(EditorStyles.objectFieldThumb);
        Space(pixel);
    }

    private void EndOutBorder()
    {
        Space(10f);
        GUILayout.EndVertical();
    }
    #endregion

    #region InnerBorder
    private void BeginInnerBorder(float pixel = 10f)
    {
        GUILayout.BeginHorizontal();
        Space(pixel);
        GUILayout.BeginHorizontal(EditorStyles.helpBox);
    }

    private void EndInnerBorder()
    {
        GUILayout.EndHorizontal();
        Space(10f);
        GUILayout.EndHorizontal();
    }
    #endregion

    private void Space(float pixel)
    {
        GUILayout.Space(pixel);
    }
}