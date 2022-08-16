using MD.EnumDictionary;
using UnityEngine;

public class TileDictionary : EnumDictionary<TileType, GameObject, Color>
{
    //_T1 => custom dictionary derived from base class
    public (GameObject Prefab, Color Color) GetDictionaryValues(TileType key)
    {
        return GetValues(key);  // `GetValues(key)` => returns custom dictionary values, derived from base class
    }
    public void SetTileColor(TileType key)
    {
        if (GetDictionaryValues(key).Prefab.TryGetComponent(out Renderer renderer))
            renderer.sharedMaterial.color = GetDictionaryValues(key).Color;
        else
            Debug.LogWarning($"{GetDictionaryValues(key).Prefab} has no renderer!");
    }
}
