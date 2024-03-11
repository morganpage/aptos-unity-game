using UnityEngine;
using UnityEngine.Tilemaps;
using System;


[System.Serializable]
public class GameTile
{
  public static Action<GameTile> OnGameTileSelected;
  public string name;
  public int movementCost = 1;
  public Vector2Int _position;
  public Vector3 worldPosition;
  public GameUnit gameUnit;
  public GameCity gameCity;
  public GameGatherable gameGatherable;
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

  public Vector2Int Position
  {
    get => _position;
    set
    {
      _position = value;
    }
  }



}