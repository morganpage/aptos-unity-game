using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TutorialStep
{
  public string title;
  public string description;
  public ObjectiveData objectiveData;
}


[CreateAssetMenu(fileName = "TutorialData", menuName = "GAME/TutorialData", order = 1)]
public class TutorialData : ScriptableObject
{
  public List<TutorialStep> steps;
}
