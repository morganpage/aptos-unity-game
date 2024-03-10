using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
  [SerializeField] private UIGameInfo uiGameInfo;

  void OnEnable()
  {
    GameCity.OnGameCityLevelUpdated += OnGameCityLevelUpdated;
  }

  void OnDisable()
  {
    GameCity.OnGameCityLevelUpdated -= OnGameCityLevelUpdated;
  }


  void OnGameCityLevelUpdated(GameCity gameCity) //Show City Level up Screen
  {
    Debug.Log("City level updated to " + gameCity.Level);
    string reward = "You can also choose a reward!";
    uiGameInfo.SetInfo("City Level Up", "City " + gameCity.Name + " has reached level " + gameCity.Level + ". " + reward);
    uiGameInfo.ClearChoices();
    uiGameInfo.AddChoice("Scouting", "Scouting", () =>
    {
      Debug.Log("Scouting");
      uiGameInfo.Show(false);
    });
    uiGameInfo.AddChoice("Hunting", "Hunting", () =>
    {
      Debug.Log("Hunting");
      uiGameInfo.Show(false);
    });
    uiGameInfo.Show(true);
  }

  [ContextMenu("TestAddChoice")]
  void TestAddChoice()
  {
    uiGameInfo.AddChoice("Influence", "10", () => Debug.Log("Influence-10"));
  }

}
