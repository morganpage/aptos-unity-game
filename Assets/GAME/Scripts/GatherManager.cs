using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherManager : MonoBehaviour
{
  [SerializeField] private Tweener _tweener;
  [SerializeField] private GameCity _gameCity;

  void OnEnable()
  {
    GameGatherable.OnGameGatherableGathered += OnGameGatherableGathered;
  }

  void OnDisable()
  {
    GameGatherable.OnGameGatherableGathered -= OnGameGatherableGathered;
  }

  void OnGameGatherableGathered(GameGatherable gameGatherable)
  {
    GameWorld.Influence--;
    _tweener.StartTween(2);
    _gameCity.AddPopulation();
  }
}
