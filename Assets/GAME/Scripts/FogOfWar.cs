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
    GameCity.OnGameCityPositionUpdated += HandleGameCityPositionUpdated;
  }

  private void OnDisable()
  {
    GameUnit.OnGameUnitMoved -= HandleGameUnitMoved;
    GameCity.OnGameCityPositionUpdated -= HandleGameCityPositionUpdated;
  }

  private void HandleGameUnitMoved(GameUnit gameUnit)
  {
    UpdateFogOfWar(gameUnit.Position);

    // Debug.Log($"FogOfWar: {gameUnit.name} at {gameUnit.position}");
    // Vector3Int tilePosition = new Vector3Int(gameUnit.position.x, gameUnit.position.y, 0);
    // Tile tile = _tilemap.GetTile<Tile>(tilePosition);
    // _tilemap.SetTile(tilePosition, null);
    // Vector2Int[] neighbors = TileUtils.Neighbors(gameUnit.position);
    // foreach (Vector2Int neighbor in neighbors)
    // {
    //   Vector3Int neighborTilePosition = new Vector3Int(neighbor.x, neighbor.y, 0);
    //   _tilemap.SetTile(neighborTilePosition, null);
    //   GameWorld.Tiles[neighbor].FogOfWar = false;
    //   //GameWorld.RemoveFogOfWar(neighbor);
    //   //GameWorld.Tiles[neighbor] = null;
    //   //if (GameWorld.Tiles != null && GameWorld.Tiles.ContainsKey(neighbor)) GameWorld.Tiles[neighbor] = null;

    //   // Tile neighborTile = _tilemap.GetTile<Tile>(neighborTilePosition);
    //   // if (neighborTile == null) continue;
    //   // _tilemap.SetTileFlags(neighborTilePosition, TileFlags.None);
    //   // _tilemap.SetColor(neighborTilePosition, Color.gray);
    // }
  }

  private void HandleGameCityPositionUpdated(GameCity gameCity)
  {
    UpdateFogOfWar(gameCity.Position);
    // Debug.Log($"FogOfWar: {gameCity.name} at {gameCity.Position}");
    // Vector3Int tilePosition = new Vector3Int(gameCity.Position.x, gameCity.Position.y, 0);
    // Tile tile = _tilemap.GetTile<Tile>(tilePosition);
    // _tilemap.SetTile(tilePosition, null);
    // Vector2Int[] neighbors = TileUtils.Neighbors(gameCity.Position);
    // foreach (Vector2Int neighbor in neighbors)
    // {
    //   Vector3Int neighborTilePosition = new Vector3Int(neighbor.x, neighbor.y, 0);
    //   _tilemap.SetTile(neighborTilePosition, null);
    //   GameWorld.Tiles[neighbor].FogOfWar = false;
    // }
  }

  private void UpdateFogOfWar(Vector2Int start)
  {
    Vector3Int startTilePosition = new Vector3Int(start.x, start.y, 0);
    _tilemap.SetTile(startTilePosition, null);
    GameWorld.Tiles[start].FogOfWar = false;
    Vector2Int[] neighbors = TileUtils.Neighbors(start);
    foreach (Vector2Int neighbor in neighbors)
    {
      Vector3Int neighborTilePosition = new Vector3Int(neighbor.x, neighbor.y, 0);
      _tilemap.SetTile(neighborTilePosition, null);
      GameWorld.Tiles[neighbor].FogOfWar = false;
    }
  }


}
