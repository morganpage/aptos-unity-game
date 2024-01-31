using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.iOS;

public class UISelected : MonoBehaviour
{
  [SerializeField] private GameObject _panel;
  [SerializeField] private Image _image;
  [SerializeField] private TextMeshProUGUI _textName;
  [SerializeField] private TextMeshProUGUI _textDescription;
  [SerializeField] private GameObject _selector;

  void Awake()
  {
    _panel.SetActive(false);
    _selector.SetActive(false);
  }


  private void OnEnable()
  {
    GameTile.OnGameTileSelected += HandleGameTileSelected;
    GameItemSelector.OnGameItemSelected += HandleGameItemSelected;
    GameItemSelector.OnGameItemUnSelected += HandleGameItemUnSelected;
  }

  private void OnDisable()
  {
    GameTile.OnGameTileSelected -= HandleGameTileSelected;
    GameItemSelector.OnGameItemSelected -= HandleGameItemSelected;
    GameItemSelector.OnGameItemUnSelected -= HandleGameItemUnSelected;
  }


  private void HandleGameItemUnSelected(GameObject gameItemSelector)
  {
    _panel.SetActive(false);
    _selector.SetActive(false);
  }

  private void HandleGameItemSelected(GameObject gameItemSelector)
  {
    if (gameItemSelector == null)
    {
      _panel.SetActive(false);
      _selector.SetActive(false);
      return;
    }
    _panel.SetActive(true);
    _selector.SetActive(gameItemSelector.GetComponent<GameUnit>() == null);//Dont show select on GameUnits
    _selector.transform.position = gameItemSelector.transform.position;
    _image.sprite = Resources.Load<Sprite>($"Sprites/{gameItemSelector.name}");
    _textName.text = gameItemSelector.name;
    _textDescription.text = $"This is a {gameItemSelector.name}. At position {gameItemSelector.transform.position}";
  }


  private void HandleGameTileSelected(GameTile gameTile)
  {
    if (gameTile == null)
    {
      _panel.SetActive(false);
      _selector.SetActive(false);
      return;
    }
    _panel.SetActive(true);
    _selector.SetActive(true);
    _selector.transform.position = gameTile.worldPosition;
    _image.sprite = Resources.Load<Sprite>($"Sprites/{gameTile.name}");
    _textName.text = gameTile.name;
    _textDescription.text = $"This is a {gameTile.name}. At position {gameTile.position}";
  }

}