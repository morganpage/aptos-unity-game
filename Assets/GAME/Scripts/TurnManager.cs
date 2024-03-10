using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
  [SerializeField] private int _startingInfluence = 10;
  [SerializeField] private Tweener _tweener;

  void OnEnable()
  {
    _tweener.OnTweenComplete += OnTweenComplete;
  }

  void OnDisable()
  {
    _tweener.OnTweenComplete -= OnTweenComplete;
  }

  void OnTweenComplete(bool final)
  {
    GameWorld.Influence++;
  }

  [ContextMenu("End Turn")]
  public void EndTurn()
  {
    _tweener.StartTween(_startingInfluence);
  }




}
