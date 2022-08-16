/*
 * Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, July 2022
*/
using MD.EnumDictionary;
using UnityEngine;

public class TileDictionary : EnumDictionary<TileType, GameObject, Color>
{
    //_T1 => custom dictionary derived from base class
    public (GameObject Prefab, Color Color) GetDictionaryValues(TileType key)
    {
        return GetValues(key);  // `GetValues(key)` => returns custom dictionary values, inherited  from base class
    }
    public void SetTileColor(TileType key)
    {
        if (GetDictionaryValues(key).Prefab.TryGetComponent(out Renderer renderer))
            renderer.sharedMaterial.color = GetDictionaryValues(key).Color;
        else
            Debug.LogWarning($"{GetDictionaryValues(key).Prefab} has no renderer!");
    }
}
