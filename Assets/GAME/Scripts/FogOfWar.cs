using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;


public class FogOfWar : MonoBehaviour
{
  [SerializeField] private Tilemap _tilemap;

  private void OnEnable()
  {
    GameUnit.OnGameUnitMoved += HandleGameUnitMoved;
  }

  private void OnDisable()
  {
    GameUnit.OnGameUnitMoved -= HandleGameUnitMoved;
  }

  private void HandleGameUnitMoved(GameUnit gameUnit)
  {
    Debug.Log($"FogOfWar: {gameUnit.name} at {gameUnit.position}");
    Vector3Int tilePosition = new Vector3Int(gameUnit.position.x, gameUnit.position.y, 0);
    Tile tile = _tilemap.GetTile<Tile>(tilePosition);
    _tilemap.SetTile(tilePosition, null);
    Vector2Int[] neighbors = TileUtils.Neighbors(gameUnit.position);
    foreach (Vector2Int neighbor in neighbors)
    {
      Vector3Int neighborTilePosition = new Vector3Int(neighbor.x, neighbor.y, 0);
      _tilemap.SetTile(neighborTilePosition, null);
      GameWorld.Tiles[neighbor].FogOfWar = false;
      //GameWorld.RemoveFogOfWar(neighbor);
      //GameWorld.Tiles[neighbor] = null;
      //if (GameWorld.Tiles != null && GameWorld.Tiles.ContainsKey(neighbor)) GameWorld.Tiles[neighbor] = null;

      // Tile neighborTile = _tilemap.GetTile<Tile>(neighborTilePosition);
      // if (neighborTile == null) continue;
      // _tilemap.SetTileFlags(neighborTilePosition, TileFlags.None);
      // _tilemap.SetColor(neighborTilePosition, Color.gray);
    }
  }



}
