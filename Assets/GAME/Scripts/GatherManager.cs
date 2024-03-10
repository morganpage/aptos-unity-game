using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherManager : MonoBehaviour
{
  [SerializeField] private Tweener _tweener;
  [SerializeField] private GameCity _gameCity;

  void OnEnable()
  {
    _tweener.OnTweenComplete += AddPopulationOnGatherComplete;
    GameGatherable.OnGameGatherableGathered += OnGameGatherableGathered;
  }

  void OnDisable()
  {
    GameGatherable.OnGameGatherableGathered -= OnGameGatherableGathered;
    _tweener.OnTweenComplete -= AddPopulationOnGatherComplete;
  }

  void OnGameGatherableGathered(GameGatherable gameGatherable)
  {
    _tweener.StartTween(2);
    //_gameCity.AddPopulation();
  }

  void AddPopulationOnGatherComplete(bool final)
  {
    GameWorld.Influence--;
    if (final) _gameCity.AddPopulation();
  }

}
