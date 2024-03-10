using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
  [SerializeField] private UIGameInfo _uiGameInfo;
  [SerializeField] private TurnManager _turnManager;
  [SerializeField] private TutorialData _tutorialData;
  [SerializeField] private ObjectiveController _objectiveController;
  public int _currentStep = 0;

  void Start()
  {
    Debug.Log("TutorialManager Start");
    ShowCurrentStep();
    // _uiGameInfo.SetInfo("Welcome to the Tutorial", "This is a tutorial campaign to teach you the ropes! First you need to harvest some fruit to feed your people. I'll give you 10 influence to get started.");
    // _uiGameInfo.ClearChoices();
    // _uiGameInfo.AddChoice("Influence", "10", () => StartTutorial());
    //_uiGameInfo.Show(true);
  }

  void ShowCurrentStep()
  {
    Debug.Log("ShowCurrentStep");
    _uiGameInfo.ClearChoices();
    _uiGameInfo.SetInfo(_tutorialData.steps[_currentStep].title, _tutorialData.steps[_currentStep].description);
    _uiGameInfo.AddChoice("Influence", "10", () => StartTutorial());
    _objectiveController.AddObjectiveSteps(_tutorialData.steps[_currentStep].objectiveData.objectives);
    // foreach (var choice in _tutorialData.steps[_currentStep].choices)
    // {
    //   _uiGameInfo.AddChoice(choice.spriteName, choice.text, choice.action);
    // }
    //_uiGameInfo.Show(true);
  }

  void StartTutorial()
  {
    Debug.Log("StartTutorial");
    _turnManager.EndTurn();
    _uiGameInfo.Show(false);
  }

  [ContextMenu("TestSave")]
  void TestSave()
  {
    Debug.Log("TestSave");
    GameWorld.Save();
  }

  [ContextMenu("TestLoad")]
  void TestLoad()
  {
    Debug.Log("TestLoad");
    GameWorld.Load();
  }

}
