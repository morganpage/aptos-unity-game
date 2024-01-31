using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveVisuals : MonoBehaviour
{
  [SerializeField] private GridManager _gridManager;
  [SerializeField] private Vector2Int _gameUnit;
  private GameObject[] _moveIndicators;
  private int maxIndicators = 10;
  void Awake()
  {
    GameObject moveIndicatorPrefab = Resources.Load<GameObject>("Prefabs/MoveIndicator");
    _moveIndicators = new GameObject[maxIndicators];
    for (int i = 0; i < maxIndicators; i++)
    {
      _moveIndicators[i] = Instantiate(moveIndicatorPrefab, Vector3.zero, Quaternion.identity, transform);
      _moveIndicators[i].SetActive(false);
    }
  }

  private void OnEnable()
  {
    //GameUnit.OnGameUnitSelected += HandleGameUnitSelected;
    GameItemSelector.OnGameItemSelected += HandleGameItemSelected;
  }

  private void OnDisable()
  {
    //GameUnit.OnGameUnitSelected -= HandleGameUnitSelected;
    GameItemSelector.OnGameItemSelected -= HandleGameItemSelected;
  }





  private void HandleGameItemSelected(GameObject gameItem)
  {
    GameUnit gameUnit = gameItem?.GetComponent<GameUnit>();

    if (gameUnit == null)
    {
      for (int j = 0; j < maxIndicators; j++)
      {
        _moveIndicators[j].SetActive(false);
      }
      return;
    }
    var ValidMoveGameTiles = GameWorld.ValidMoves(gameUnit.position);
    int i = 0;
    foreach (var tile in ValidMoveGameTiles)
    {
      _moveIndicators[i].SetActive(true);
      _moveIndicators[i].transform.position = tile.worldPosition;
      i++;
    }
    for (; i < maxIndicators; i++)
    {
      _moveIndicators[i].SetActive(false);
    }
  }

}
