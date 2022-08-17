# Grid Generator
An editor tool that provide you to easily design levels in grid-based games

|<b>Grid Generator Editor Tool</b><br>|<b>First look</b><br>|
|:----:|:----:|
|<img src="/.github/screenshots/title.png">|![](/.github/screenshots/00.gif)|

## Usage
<!--1. Add `GridGenerator` script to your scene.
2. Create a `Grid Data` from `MD/GridData` and assign it to `GridGenerator`
3. Set grid size and create grid layout by clicking `Generate` button
4. Change tile type by clicking them on it. And click `Create Grid` or `Create Grid as Prefab` button to create Grid.
5. It's saves data to scriptable object automatically.*/-->

| 1. Add `GridGenerator` script to your scene.                                    |  2. Create a `Grid Data` from `Create/MD/GridData` and assign it to `GridGenerator` |
|:---:|:---:|
| <img src="/.github/screenshots/0.png">  |  <img src="/.github/screenshots/0.1.png"> |
| 3. Set grid size and create grid layout by clicking `Generate` button           | 4. Change tile type by clicking them on it. And click `Create Grid` or `Create Grid as Prefab` button to create Grid. |
| <img src="/.github/screenshots/1.png"> |<img src="/.github/screenshots/1.1.png"> |

|Tutorial|
|:---:|
|![](/.github/screenshots/11.gif)|



## Tile and Button Settings
 * "Custom Enum Dictionary" is used in this project. Basically, it stores data and returns values with given enum key. it's serializable
 * For each tile type, there are tile prefabs and colors field (for both of button and tile colors). Find the `EnumDictionary` prefab under the project folder to set your own data with serialized dictionary.  
 * To add or remove tile types, edit the 'TileType' enumeration in the 'GridGenerator' script. (Dictionary updates itself automatically)  

|Enum Dictionary Inspector|
|:---:|
|<img src="/.github/screenshots/data.png"> |

### If you want to quick look to `Custom Enum Dictionary`
[<img src="/.github/screenshots/temp.png">](https://github.com/mmtdgn/enum-dictionary)  

## Grid Data

When you create a grid, this file stores data.

|Tile Dictionary|TileDictionary reference|
|:--------:|----------|
|Spawn Distance |The distance to leave between each tile when creating the grid|
