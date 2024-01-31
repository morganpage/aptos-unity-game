using UnityEngine;
using UnityEngine.Tilemaps;
using System;


[System.Serializable]
public class GameTile
{
  public static Action<GameTile> OnGameTileSelected;
  public string name;
  public int movementCost = 1;
  public Vector2Int position;
  public Vector3 worldPosition;
  public GameUnit gameUnit;
  public GameCity gameCity;

  public bool FogOfWar = true;
  public static GameTile _selected;

  public static GameTile Selected
  {
    get => _selected;
    set
    {
      _selected = value;
      OnGameTileSelected?.Invoke(_selected);
    }
  }
}