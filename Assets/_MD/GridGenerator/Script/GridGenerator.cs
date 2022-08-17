/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum TileType { T0, T1, T2, T3, T4 }
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GridData m_GridData;
    public GridData GridData { get => m_GridData; set => m_GridData = value; }
    [SerializeField] private int m_X = 5;
    public int X { get => m_X; set => m_X = value; }
    [SerializeField] private int m_Y = 5;
    public int Y { get => m_Y; set => m_Y = value; }

    [SerializeField] private TileSettings[,] m_GridCells;
    public TileSettings[,] GridCells { get => m_GridCells; set => m_GridCells = value; }
    public bool IsGridDataNull { get => m_GridData == null ? true : false; }
    public List<GameObject> Tiles;
    [SerializeField] private Transform m_Grid;
    public Transform Grid { get => m_Grid; set => m_Grid = value; }

    public void UpdateGrid(int Length)
    {
        if (!IsGridDataNull)
            m_GridData.Init();
        else
            Debug.LogWarning("Grid Data has not been set! To save your progress, please assign Grid Data!");

        m_GridCells = new TileSettings[m_X, m_Y];

        for (var i = 0; i < m_X; i++)
        {
            for (var j = 0; j < m_Y; j++)
            {
                m_GridCells[i, j] = new TileSettings
                {
                    name = $"{i.ToString()}, {j.ToString()}",
                    TileType = TileType.T0,
                    Coordinate =
                    {
                        x = i,
                        y = j
                    }
                };

                if (!IsGridDataNull)
                    m_GridData.SetCellData(m_GridCells[i, j]);
            }
        }
    }

    public void AutoUpdate()
    {
        if (IsGridDataNull) return;

        if (m_GridCells.GetUpperBound(0) != m_GridData.GridSize.x ||
        m_GridCells.GetUpperBound(1) != m_GridData.GridSize.y)
        {
            m_X = GridData.GridSize.x;
            m_Y = GridData.GridSize.y;
        }
    }

    public void GetGridData()
    {
        if (IsGridDataNull) return;

        int _x = m_GridData.GridSize.x;
        int _y = m_GridData.GridSize.y;

        m_GridCells = new TileSettings[_x, _y];

        for (var i = 0; i < _x; i++)
        {
            for (var j = 0; j < _y; j++)
            {
                if (i >= m_GridData.GridSize.x || j >= m_GridData.GridSize.y)
                    break;
                m_GridCells[i, j] = m_GridData.GetCellData(i, j);
            }
        }
    }

    public Color GetColor(int x, int y)
    {
        if (IsGridDataNull) return Color.gray;

        if (m_GridCells == null)
            return Color.gray;
        if (x > m_GridCells.GetUpperBound(0) || y > m_GridCells.GetUpperBound(1))
        {
            Debug.LogWarning("Out of Bounds!,Please Regenerate Grid!");
            return Color.gray;
        }
        return m_GridData.GetTileColor(x, y);
    }

    public void ChangeState(int x, int y)
    {
        if (m_GridCells == null)
            return;
        if (x > m_GridCells.GetUpperBound(0) || y > m_GridCells.GetUpperBound(1))
        {
            Debug.LogWarning("Out of Bounds!,Please Regenerate Grid!");
            return;
        }
        int _typeIndex = (int)m_GridCells[x, y].TileType;
        _typeIndex++;

        if (_typeIndex == Enum.GetNames(typeof(TileType)).Length)
            _typeIndex = 0;

        m_GridCells[x, y].TileType = (TileType)_typeIndex;
    }

    public string GetName(int _x, int _y)
    {
        if (m_GridCells == null)
            return string.Empty;
        if (_x > m_GridCells.GetUpperBound(0) || _y > m_GridCells.GetUpperBound(1))
        {
            Debug.LogWarning("Out of Bounds!,Please Regenerate Grid!");
            return string.Empty;
        }
        return m_GridCells[_x, _y].name;
    }

    public string GetTypeName(int _x, int _y)
    {
        if (m_GridCells == null)
            return string.Empty;
        if (_x > m_GridCells.GetUpperBound(0) || _y > m_GridCells.GetUpperBound(1))
        {
            Debug.LogWarning("Out of Bounds!,Please Regenerate Grid!");
            return string.Empty;
        }
        return Enum.GetName(typeof(TileType), m_GridCells[_x, _y].TileType);
    }

    public void SetGridSize(int x, int y)
    {
        if (IsGridDataNull) return;
        m_GridData.GridSize.Set(x, y);
    }

    public void UpdateGridSizeField()
    {
        if (m_GridData == null) return;

        m_X = m_GridData.GridSize.x;
        m_Y = m_GridData.GridSize.y;
    }

    public void UpdateTileColors()
    {
        for (var i = 0; i <= m_GridCells.GetUpperBound(0); i++)
        {
            for (var j = 0; j <= m_GridCells.GetUpperBound(1); j++)
            {
                m_GridData.SetTileColors(i, j);
            }
        }
    }

    public void RandomGenerate()
    {
        if (IsGridDataNull) return;

        int _x = m_GridData.GridSize.x;
        int _y = m_GridData.GridSize.y;
        int _randomValue = 0;

        m_GridCells = new TileSettings[_x, _y];

        for (var i = 0; i < _x; i++)
        {
            for (var j = 0; j < _y; j++)
            {
                _randomValue = UnityEngine.Random.Range(1, Enum.GetNames(typeof(TileType)).Length);
                // => T0 (Empty Tile) not include. To include it set Range() minInclisuve parameter to 0;

                if (i >= m_GridData.GridSize.x || j >= m_GridData.GridSize.y)
                    break;
                m_GridCells[i, j] = m_GridData.GetCellData(i, j);
                m_GridCells[i, j].TileType = (TileType)(_randomValue);
            }
        }
    }

    public void CreateGrid(bool IsPrefabMode = false)
    {
        int _x = m_GridCells.GetUpperBound(0) + 1;
        int _y = m_GridCells.GetUpperBound(1) + 1;
        float _spawnDistance = m_GridData.SpawnDistance;

        GameObject _GridRoot = new GameObject()
        {
            transform =
            {
                name = $"Grid_{_x}x{_y}",
                position = Vector3.zero
            }
        };

        Vector3 centerPostion = Vector3.zero;
        if (m_Grid)
            DestroyImmediate(m_Grid.gameObject);
        for (var i = 0; i < _x; i++)
        {
            for (var j = 0; j < _y; j++)
            {
                Vector3 _tilePosition = Vector3.right * i * _spawnDistance + Vector3.forward * j * _spawnDistance;

                GameObject _tile = null;
                var _prefab = m_GridData.GetTilePrefab(i, j);

                if (IsPrefabMode)
                {
#if UNITY_EDITOR
                    _tile = (GameObject)PrefabUtility.InstantiatePrefab(_prefab);
                    _tile.transform.position = _tilePosition;
#endif
                }
                else
                {
                    _tile = Instantiate(_prefab, _tilePosition, Quaternion.identity);
                }

                _tile.transform.SetParent(_GridRoot.transform);
                _tile.name = $"Tile ({i} x {j})";
                m_Grid = _GridRoot.transform;
                SetCenter(_tile, _x, _y);
            }
        }

        void SetCenter(GameObject _tile, int _x, int _Y)
        {
            _tile.transform.position += Vector3.left * (_x - 1) * _spawnDistance / 2
            + Vector3.back * (_y - 1) * _spawnDistance / 2;
        }
    }
}
