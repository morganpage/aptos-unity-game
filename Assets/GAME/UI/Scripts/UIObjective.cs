using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIObjective : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _objectiveDescriptionText;
  [SerializeField] private TextMeshProUGUI _objectiveProgressText;
  //[SerializeField] private Toggle _objectiveToggle;
  private string _objectiveName;
  private int _objectiveProgress;
  private int _objectiveMaxProgress;

  public string Name { get => _objectiveName; }

  public int Progress
  {
    get => _objectiveProgress;
    set
    {
      _objectiveProgress = value;
      _objectiveProgressText.text = _objectiveProgress + "/" + _objectiveMaxProgress;
      _objectiveProgressText.color = _objectiveProgress >= _objectiveMaxProgress ? Color.green : Color.white;
      _objectiveDescriptionText.color = _objectiveProgress >= _objectiveMaxProgress ? Color.green : Color.white;
    }
  }

  void Awake()
  {
    //_objectiveToggle.isOn = false;
  }

  public void SetObjective(ObjectiveStep objective)
  {
    _objectiveDescriptionText.text = objective.description;
    _objectiveName = objective.name;
    _objectiveMaxProgress = objective.maxProgress;
    Progress = 0;
  }

}
