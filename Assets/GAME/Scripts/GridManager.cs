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



      // if (GameTile.Selected == gameTile.gameObject)
      // {
      //   GameItemSelector.Selected = null;//Unselect the tile and move on
      // }
      // else
      // {
      //   GameItemSelector.Selected = gameTile.gameObject;
      // }


      // if (gameTile.gameUnit != null)
      // {
      //   print($"Clicked unit: {gameTile.gameUnit.name} at {gridPosition}");
      //   if (GameUnit.Selected != null)
      //   {
      //     GameUnit.Selected = null;
      //     GameTile.Selected = gameTile;
      //     return;
      //   }
      //   else
      //   {
      //     GameUnit.Selected = gameTile.gameUnit;
      //     GameTile.Selected = null;
      //     return;
      //   }
      // }
      // else
      // {
      //   Debug.Log($"GridManager: {gameTile.name} at {gameTile.worldPosition}");
      //   GameTile.Selected = GameTile.Selected == gameTile ? null : gameTile;
      // }



      // if (GameTile.Selected == gameTile)
      // {
      //   //gameTile.Selected = false;
      //   GameTile.Selected = null;
      //   _selector.gameObject.SetActive(false);
      //   //Deselected the tile, is there a unit to select now?
      //   if (gameTile.gameUnit != null)
      //   {
      //     Debug.Log($"GridManager: {gameTile.gameUnit.name} at {gameTile.gameUnit.position}");
      //     gameTile.gameUnit.Selected = true;
      //     return;//So not overiden by gametile selected
      //   }
      // }
      // else
      // {
      //   if (gameTile.gameUnit != null)
      //   {
      //     Debug.Log($"GridManager: {gameTile.gameUnit.name} at {gameTile.gameUnit.position}");
      //     gameTile.gameUnit.Selected = false;
      //   }
      //   //gameTile.Selected = true;
      //   GameTile.Selected = gameTile;
      //   _selector.position = gameTile.worldPosition;
      //   _selector.gameObject.SetActive(true);
      // }
      // OnGameTileSelected?.Invoke(gameTile, GameTile.Selected == gameTile);
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
