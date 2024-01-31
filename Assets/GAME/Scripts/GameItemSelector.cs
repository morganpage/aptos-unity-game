using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameItemSelector : MonoBehaviour
{
  public static Action<GameObject> OnGameItemSelected;
  public static Action<GameObject> OnGameItemUnSelected;
  private static GameObject _selected;
  public static GameObject Selected
  {
    get => _selected;
    set
    {
      if (_selected != null) OnGameItemUnSelected?.Invoke(_selected);
      _selected = value;
      OnGameItemSelected?.Invoke(_selected);
    }
  }

}
