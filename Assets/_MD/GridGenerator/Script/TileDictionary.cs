using System.Collections;
using System.Collections.Generic;
using MD.EnumDictionary;
using MD.EnumDictionary.Extensions;
using UnityEngine;

public class TileDictionary : EnumDictionary<TileType, GameObject, Color>
{
    //_T1 => custom dictionary derived from base class
    public (GameObject Prefab, Color color) GetValues(TileType key)
    {
        return _T1.GetValues(key);
    }
    public void SetTileColor(TileType key)
    {
        if (_T1.GetFirstValue(key).TryGetComponent(out Renderer renderer))
            renderer.sharedMaterial.color = GetValues(key).color;
        else
            Debug.LogWarning($"{_T1.GetFirstValue(key)} has no renderer!");
    }
}
