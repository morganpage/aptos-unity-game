using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ObjectiveStep
{
  public enum ObjectiveType { GATHER, TRAIN }
  public string description;
  public ObjectiveType type;
  public string name;//Name of the gatherable,unit etc
  public int maxProgress;
}



[CreateAssetMenu(fileName = "ObjectiveData", menuName = "GAME/ObjectiveData", order = 2)]
public class ObjectiveData : ScriptableObject
{
  public List<ObjectiveStep> objectives;
}
