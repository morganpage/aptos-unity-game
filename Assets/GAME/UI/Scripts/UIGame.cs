using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIGame : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _turnText;
  [SerializeField] private TextMeshProUGUI _influenceText;


  void OnEnable()
  {
    GameWorld.OnTurnChanged += HandleTurnChanged;
    GameWorld.OnInfluenceChanged += HandleInfluenceChanged;

  }

  void OnDisable()
  {
    GameWorld.OnTurnChanged -= HandleTurnChanged;
    GameWorld.OnInfluenceChanged -= HandleInfluenceChanged;
  }

  void Awake()
  {
    _turnText.text = "-";
    _influenceText.text = "-";
  }

  private void HandleTurnChanged(int turn)
  {
    _turnText.text = $"{turn}";
  }

  void HandleInfluenceChanged(int influence)
  {
    _influenceText.text = influence.ToString();
  }



  [ContextMenu("Save Game")]
  public void SaveGame()
  {
    GameWorld.Save();
  }

}
