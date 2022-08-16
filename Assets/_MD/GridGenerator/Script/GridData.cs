/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
*/
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GridData", menuName = "MD/GridData", order = 0)]
[System.Serializable]
public class GridData : ScriptableObject
{
    public TileDictionary TileDictionary;
    public float SpawnDistance;
    [HideInInspector] public Vector2Int GridSize;
    [Header("Tile Data")]
    public List<Tile> CellSettings;

    public void Init()
    {
        CellSettings.Clear();
        CellSettings = new List<Tile>(new Tile[GridSize.x]);

        for (var i = 0; i < GridSize.x; i++)
        {
            CellSettings[i] = new Tile()
            {
                Y = new List<TileSettings>(new TileSettings[GridSize.y])
            };
        }
    }

    public void SetCellData(TileSettings cell)
    {
        CellSettings[cell.Coordinate.x].Y[cell.Coordinate.y] = cell;
    }

    public TileSettings GetCellData(int x, int y)
    {
        return CellSettings[x].Y[y];
    }

    public Color GetTileColor(int x, int y)
    {
        return TileDictionary.GetDictionaryValues(GetTileKey(x, y)).Color;
    }
    public GameObject GetTilePrefab(int x, int y)
    {
        return TileDictionary.GetDictionaryValues(GetTileKey(x, y)).Prefab;
    }

    public void SetTileColors(int x, int y)
    {
        TileDictionary.SetTileColor(GetTileKey(x, y));
    }

    private TileType GetTileKey(int x, int y)
    {
        return GetCellData(x, y).TileType;
    }
}

[System.Serializable]
public class Tile
{
    [HideInInspector] public string name = "X";
    public List<TileSettings> Y = new List<TileSettings>();
}

[System.Serializable]
public class TileSettings
{
    public string name;
    public Vector2Int Coordinate;
    public TileType TileType;
}