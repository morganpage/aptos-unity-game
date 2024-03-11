using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameCity : MonoBehaviour
{
  [SerializeField] private SpriteRenderer _spriteRenderer;
  [SerializeField] private TextMeshProUGUI _name;
  [SerializeField] private TextMeshProUGUI _level;

  [SerializeField] private Image _populationProgress;
  private GameCity _gameCity;

  void Awake()
  {
    _gameCity = GetComponent<GameCity>();
    _name.text = _gameCity.Name;
    _spriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/{gameObject.name}");
    _populationProgress.fillAmount = 0;
  }

  void OnEnable()
  {
    _gameCity.OnGameCityUpdated += OnGameCityUpdated;
  }

  void OnDisable()
  {
    _gameCity.OnGameCityUpdated -= OnGameCityUpdated;
  }

  void OnGameCityUpdated(GameCity gameCity)
  {
    _level.text = gameCity.Level.ToString();
    int progress = _gameCity.Population - _gameCity.LevelToPopulationNeeded(_gameCity.Level);
    int difference = _gameCity.LevelToPopulationNeeded(_gameCity.Level + 1) - _gameCity.LevelToPopulationNeeded(_gameCity.Level);
    _populationProgress.fillAmount = (float)progress / (float)difference;

    //gameObject.name = gameCity.Level == 1 ? "Village" : "Town";
    _spriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/{gameObject.name}");

  }
}
