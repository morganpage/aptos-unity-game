using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
  [SerializeField] private UIGameInfo uiGameInfo;
  [SerializeField] private Tweener _tweener;


  void OnEnable()
  {
    GameCity.OnGameCityLevelUpdated += OnGameCityLevelUpdated;
    GameCity.OnGameCitySelected += OnGameCitySelected;
    _tweener.OnTweenComplete += OnTweenComplete;
  }

  void OnDisable()
  {
    GameCity.OnGameCityLevelUpdated -= OnGameCityLevelUpdated;
    GameCity.OnGameCitySelected -= OnGameCitySelected;
    _tweener.OnTweenComplete -= OnTweenComplete;
  }
  void OnTweenComplete(bool final)
  {
    GameWorld.Influence--;
  }

  void OnGameCitySelected(GameCity gameCity)
  {
    if (gameCity == null)
    {
      uiGameInfo.Show(false);
      return;
    }
    Debug.Log("City selected " + gameCity.Name);
    uiGameInfo.SetInfo(gameCity.Name, "You can train the following units:", gameCity.gameObject.name);
    uiGameInfo.ClearChoices();
    uiGameInfo.Show(true);
    uiGameInfo.AddChoice("Rogue", "Rogue", () =>
    {
      Debug.Log("Rogue");
      GameUnit.Train("Rogue", gameCity);
      // GameObject roguePrefab = Resources.Load<GameObject>("Prefabs/Rogue");
      // GameObject rogue = Instantiate(roguePrefab, gameCity.transform.position, Quaternion.identity);
      // rogue.GetComponent<GameUnit>().Position = gameCity.Position;
      _tweener.StartTween(4);
      uiGameInfo.Show(false);
    });
  }



  void OnGameCityLevelUpdated(GameCity gameCity) //Show City Level up Screen
  {
    Debug.Log("City level updated to " + gameCity.Level);
    string reward = "You can also choose a reward!";
    uiGameInfo.SetInfo("City Level Up", "City " + gameCity.Name + " has reached level " + gameCity.Level + ". " + reward, gameCity.gameObject.name);
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
