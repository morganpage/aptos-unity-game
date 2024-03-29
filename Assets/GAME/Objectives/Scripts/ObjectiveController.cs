using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
  [SerializeField] private GameObject _uiObjectivePrefab;
  [SerializeField] private Transform _uiObjectiveParent;

  void Awake()
  {
    foreach (Transform child in _uiObjectiveParent)
    {
      Destroy(child.gameObject);
    }
  }

  void OnEnable()
  {
    GameGatherable.OnGameGatherableGathered += OnGameGatherableGathered;
    GameUnit.OnGameUnitTrained += OnGameUnitTrained;
  }

  void OnDisable()
  {
    GameGatherable.OnGameGatherableGathered -= OnGameGatherableGathered;
    GameUnit.OnGameUnitTrained -= OnGameUnitTrained;
  }

  void OnGameGatherableGathered(GameGatherable gatherable)
  {
    Debug.Log("Gathered: " + gatherable.name);
    UIObjective[] uiObjectives = FindObjectsOfType<UIObjective>();
    foreach (UIObjective uiObjective in uiObjectives)
    {
      if (uiObjective.Name == gatherable.name)
      {
        uiObjective.Progress++;
      }
    }
    RefreshArrows();
  }

  void OnGameUnitTrained(GameUnit gatherable)
  {
    Debug.Log("OnGameUnitTrained: " + gatherable.name);
    UIObjective[] uiObjectives = FindObjectsOfType<UIObjective>();
    foreach (UIObjective uiObjective in uiObjectives)
    {
      if (uiObjective.Name == gatherable.name)
      {
        uiObjective.Progress++;
      }
    }
    RefreshArrows();
  }


  public void AddObjectiveSteps(List<ObjectiveStep> objectives)
  {
    Debug.Log("Adding objectives: " + objectives.Count);
    foreach (ObjectiveStep objective in objectives)
    {
      GameObject uiObjective = Instantiate(_uiObjectivePrefab, _uiObjectiveParent);
      UIObjective uiObjectiveComponent = uiObjective.GetComponent<UIObjective>();
      uiObjectiveComponent.SetObjective(objective);
      //_uiObjectives.Add(uiObjectiveComponent);
    }
    RefreshArrows();
  }

  void RefreshArrows()
  {
    foreach (Transform child in _uiObjectiveParent)
    {
      UIObjective uiObjective = child.GetComponent<UIObjective>();
      if (uiObjective == null) continue;
      if (uiObjective.Progress < uiObjective.MaxProgress)
      {
        uiObjective.SetArrow(true);
        return;
      }
    }
  }

}
