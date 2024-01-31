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
      if (_selected != null)
      {
        IOutline outline = _selected.GetComponent<IOutline>();
        if (outline != null) outline.Outline = false;
        OnGameItemUnSelected?.Invoke(_selected);
      }
      _selected = value;
      if (_selected != null)
      {
        IOutline outline = _selected.GetComponent<IOutline>();
        if (outline != null) outline.Outline = true;
      }
      OnGameItemSelected?.Invoke(_selected);
    }
  }




}
