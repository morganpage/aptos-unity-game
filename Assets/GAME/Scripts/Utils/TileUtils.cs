using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUtils
{

  public static Vector2Int[] Neighbors(Vector2Int position)
  {
    Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1) };
    Vector2Int[] neighbors = new Vector2Int[directions.Length];
    for (int i = 0; i < directions.Length; i++)
    {
      neighbors[i] = position + directions[i];
    }
    return neighbors;
  }


}
