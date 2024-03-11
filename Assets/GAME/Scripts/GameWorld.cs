using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class GameWorld //Hold the current state of the game world
{
  public static Action<int> OnInfluenceChanged;

  public static Action<int> OnTurnChanged;
  public static int _influence;
  public static int _turn;

  public static int Influence
  {
    get => _influence;
    set
    {
      _influence = value;
      OnInfluenceChanged?.Invoke(_influence);
    }
  }

  public static int Turn
  {
    get => _turn;
    set
    {
      _turn = value;
      OnTurnChanged?.Invoke(_turn);
    }
  }



  public static Dictionary<Vector2Int, GameTile> Tiles { get { return _tiles; } }
  private static Dictionary<Vector2Int, GameTile> _tiles;

  public static void Save()
  {
    GameData gameData = new GameData();//Create a new GameData object and pulls in the current state of the game world
    Serializer.Save("gameWorld.dat", gameData);
  }

  public static void Load()
  {
    GameData gameData = Serializer.Load("gameWorld.dat");
    if (gameData != null)
    {
      Debug.Log("GameWorld.Load: " + gameData.Influence);
      Influence = gameData.Influence;
    }
  }


  public static void UpdateGameWorldFromTilemaps(Tilemap[] tilemaps)
  {
    _tiles = new Dictionary<Vector2Int, GameTile>();
    foreach (Tilemap tilemap in tilemaps)
    {
      foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
      {
        Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
        if (!tilemap.HasTile(localPlace)) continue;
        TileBase tile = tilemap.GetTile(localPlace);
        GameTile gameTile = new GameTile();
        gameTile.name = tile.name;
        gameTile.Position = new Vector2Int(position.x, position.y);
        gameTile.worldPosition = tilemap.CellToWorld(localPlace) + new Vector3(0.5f, 0.5f, 0);
        if (!_tiles.ContainsKey(gameTile.Position)) _tiles.Add(gameTile.Position, gameTile);
      }
    }
    //Add all GameUnits to the correct GameTile
    GameUnit[] gameUnits = GameObject.FindObjectsOfType<GameUnit>();
    foreach (GameUnit gameUnit in gameUnits)
    {
      Vector3Int gridPosition = tilemaps[0].WorldToCell(gameUnit.transform.position);
      gameUnit.Position = new Vector2Int(gridPosition.x, gridPosition.y);
      GameTile gameTile = _tiles[new Vector2Int(gridPosition.x, gridPosition.y)];
      gameTile.gameUnit = gameUnit;
      //_units.Add(gameUnit.position, gameUnit);
    }
    //Add all GameCities to the correct GameTile
    GameCity[] gameCities = GameObject.FindObjectsOfType<GameCity>();
    foreach (GameCity gameCity in gameCities)
    {
      Vector3Int gridPosition = tilemaps[0].WorldToCell(gameCity.transform.position);
      gameCity.Position = new Vector2Int(gridPosition.x, gridPosition.y);
      GameTile gameTile = _tiles[new Vector2Int(gridPosition.x, gridPosition.y)];
      gameTile.gameCity = gameCity;
    }
    //Add all GameGatherables to the correct GameTile
    GameGatherable[] gameGatherables = GameObject.FindObjectsOfType<GameGatherable>();
    foreach (GameGatherable gameGatherable in gameGatherables)
    {
      Vector3Int gridPosition = tilemaps[0].WorldToCell(gameGatherable.transform.position);
      gameGatherable.position = new Vector2Int(gridPosition.x, gridPosition.y);
      GameTile gameTile = _tiles[new Vector2Int(gridPosition.x, gridPosition.y)];
      gameTile.gameGatherable = gameGatherable;
    }
  }

  // public static void AddGameUnit(string unitName, Vector2Int position)
  // {
  //   GameObject gameUnitPrefab = Resources.Load<GameObject>($"Prefabs/{unitName}");
  //   GameObject gameUnitGameObject = GameObject.Instantiate(gameUnitPrefab);
  //   GameUnit gameUnit = gameUnitGameObject.GetComponent<GameUnit>();
  //   gameUnit.Position = position;
  //   GameItemSelector.Selected = gameUnit.gameObject;
  // }

  public static GameTile[] ValidMoves(Vector2Int startPosition)
  {
    List<GameTile> validMoves = new List<GameTile>();
    GameTile startTile = _tiles[startPosition];
    validMoves.AddRange(Neighbors(startTile));
    return validMoves.ToArray();
  }

  public static GameTile[] ValidMoves1(Vector2Int startPosition)
  {
    List<GameTile> validMoves = new List<GameTile>();
    GameTile startTile = _tiles[startPosition];
    int movementPoints = 1;
    Queue<GameTile> frontier = new Queue<GameTile>();
    frontier.Enqueue(startTile);
    while (frontier.Count > 0)
    {
      GameTile current = frontier.Dequeue();
      foreach (GameTile next in Neighbors(current))
      {
        if (next.movementCost > movementPoints) continue;
        if (validMoves.Contains(next)) continue;
        validMoves.Add(next);
        frontier.Enqueue(next);
      }
    }
    return validMoves.ToArray();
  }

  public static GameTile[] Neighbors(GameTile tile)
  {
    List<GameTile> neighbors = new List<GameTile>();
    Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1) };
    foreach (Vector2Int direction in directions)
    {
      Vector2Int neighborPosition = tile.Position + direction;

      if (_tiles.ContainsKey(neighborPosition))
      {
        GameTile neighborTile = _tiles[neighborPosition];
        if (neighborTile.name == "Sea") continue;
        if (neighborTile.name == "Mountains") continue;
        neighbors.Add(neighborTile);
      }
    }
    return neighbors.ToArray();
  }

}