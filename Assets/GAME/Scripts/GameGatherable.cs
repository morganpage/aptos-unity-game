using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGatherable : MonoBehaviour
{
  public static Action<GameGatherable> OnGameGatherableGathered;
  public Vector2Int position;

  public bool Gatherable
  {
    get
    {
      IOutline outline = GetComponent<IOutline>();
      return outline == null ? false : outline.Outline;
    }
  }

  public bool Gather()
  {
    if (Gatherable)
    {
      Debug.Log($"Gathered {name} at {position}");
      OnGameGatherableGathered?.Invoke(this);
      Destroy(gameObject);
      return true;
    }
    return false;
  }

}
