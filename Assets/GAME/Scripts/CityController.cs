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
    uiGameInfo.SetInfo("City Level Up", "City " + gameCity.Name + " has reached level " + gameCity.Level);
    uiGameInfo.Show(true);
  }
}
