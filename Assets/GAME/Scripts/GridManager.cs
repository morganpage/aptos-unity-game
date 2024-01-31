using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class GridManager : MonoBehaviour
{
  [SerializeField] private Tilemap[] _tilemaps;

  [SerializeField] private Sprite _spriteSea;

  public List<GameTile> tilesList = new List<GameTile>(); //Just for testing

  void Awake()
  {
    GameWorld.UpdateGameWorldFromTilemaps(_tilemaps);
    tilesList = new List<GameTile>(GameWorld.Tiles.Values);
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3Int gridPosition = _tilemaps[0].WorldToCell(worldPosition);
      GameTile gameTile = GameWorld.Tiles[new Vector2Int(gridPosition.x, gridPosition.y)];
      if (gameTile == null) return;
      print($"Clicked tile: {gameTile.name} at {gridPosition}");
      if (gameTile.FogOfWar)
      {
        Debug.Log($"GridManager: {gameTile.name} at {gameTile.worldPosition} is fog of war");
        return;
      }


      bool moved = MoveIfSelected(gameTile);
      if (moved) return;

      if (gameTile.gameCity != null) //First try and select a city on this tile
      {
        if (GameItemSelector.Selected == gameTile.gameCity.gameObject)
        {
          GameItemSelector.Selected = null;//Unselect the city and move on
        }
        else
        {
          GameTile.Selected = null;
          GameItemSelector.Selected = gameTile.gameCity.gameObject;
          return;
        }
      }
      //Now try and select a unit on this tile
      if (gameTile.gameUnit != null)
      {
        if (GameItemSelector.Selected == gameTile.gameUnit.gameObject)
        {
          GameItemSelector.Selected = null;//Unselect the unit and move on
        }
        else
        {
          GameTile.Selected = null;
          GameItemSelector.Selected = gameTile.gameUnit.gameObject;
          return;
        }
      }
      GameItemSelector.Selected = null;
      //Now try and select the tile
      if (GameTile.Selected == gameTile)
      {
        GameTile.Selected = null;
      }
      else
      {
        GameTile.Selected = gameTile;
      }

    }
  }

  private bool MoveIfSelected(GameTile gameTile)
  {
    //Debug.Log($"MoveIfSelected: {gameTile.name} at {gameTile.worldPosition}");
    //if (GameUnit.Selected == null) return false;
    //if (GameUnit.Selected == gameTile.gameUnit) return false;//Clicked on same tile so no move needed
    //GameUnit.Selected.MoveTo(gameTile);
    //GameUnit.DeSelectAll();
    //GameUnit.Selected = null;
    //GameUnit gameUnit = GameUnit.Selected;
    //See if there is a selected, movable unit
    GameUnit gameUnit = GameItemSelector.Selected?.GetComponent<GameUnit>();
    if (gameUnit == null) return false;
    if (gameUnit == gameTile.gameUnit) return false;//Clicked on same tile so no move needed
    gameUnit.MoveTo(gameTile);
    GameItemSelector.Selected = null;
    return true;
  }


}
