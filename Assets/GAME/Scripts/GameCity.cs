using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameCity : MonoBehaviour
{
  public string Name;
  public int _level = 1;

  public int Level
  {
    get => _level;
    set
    {
      _level = value;
      OnGameCityLevelUpdated?.Invoke(this);
    }
  }

  public int _population;

  public int Population
  {
    get => _population;
    set
    {
      _population = value;
      if (_population >= LevelToPopulationNeeded(Level + 1))
      {
        Level++;
      }
      OnGameCityUpdated?.Invoke(this);
    }
  }

  public static Action<GameCity> OnGameCitySelected;
  public static Action<GameCity> OnGameCityPositionUpdated;
  public static Action<GameCity> OnGameCityLevelUpdated;

  public Action<GameCity> OnGameCityUpdated;

  [SerializeField]
  private Vector2Int _position;

  public Vector2Int Position
  {
    get => _position;
    set
    {
      _position = value;
      //name = $"City {position.x} {position.y}";
      OnGameCityPositionUpdated?.Invoke(this);
    }
  }

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

  [ContextMenu("AddPopulation")]
  public void AddPopulation()
  {
    Population += 1;
  }

  public int LevelToPopulationNeeded(int level)
  {
    int population = 0;
    for (int i = 1; i <= level - 1; i++)
    {
      population += i + 1;
    }
    return population;
  }

  [ContextMenu("TestPopulation")]
  private void TestPopulation()
  {
    for (int i = 1; i < 10; i++)
    {
      Debug.Log($"Level {i} needs {LevelToPopulationNeeded(i)}");
    }
  }

}
