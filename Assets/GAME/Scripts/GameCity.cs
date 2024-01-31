using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameCity : MonoBehaviour
{
  public static Action<GameCity> OnGameCitySelected;
  public Vector2Int position;

  private static GameCity _selected;
  public static GameCity Selected
  {
    get => _selected;
    set
    {
      _selected = value;
      OnGameCitySelected?.Invoke(_selected);
    }
  }

}
