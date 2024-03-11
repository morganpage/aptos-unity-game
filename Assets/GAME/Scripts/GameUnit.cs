using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameUnit : MonoBehaviour
{
  public static Action<GameUnit> OnGameUnitSelected;
  public static Action<GameUnit> OnGameUnitMoved;
  public static Action<GameUnit> OnGameUnitTrained;

  public Vector2Int _position;
  private Vector2 _target;
  private IOutline _outlineSprite;
  private float _speed = 5f;
  private static GameUnit _selected;

  public Vector2Int Position
  {
    get => _position;
    set
    {
      _position = value;
      GameTile gameTile = GameWorld.Tiles[_position];
      transform.position = gameTile.worldPosition;
      _target = transform.position;
      gameTile.gameUnit = this;
    }
  }
  // public static GameUnit Selected
  // {
  //   get => _selected;
  //   set
  //   {
  //     Debug.Log($"GameUnit.Selected: {value}");
  //     if (_selected != null) _selected.GetComponent<IOutline>().Outline = false;//First un select the currently selected unit
  //     _selected = value;
  //     if (_selected != null) _selected.GetComponent<IOutline>().Outline = true;//Then select the newly selected unit
  //     OnGameUnitSelected?.Invoke(_selected);
  //   }
  // }

  void Awake()
  {
    _outlineSprite = GetComponent<IOutline>();
    _target = transform.position;
  }

  void Start()
  {
    OnGameUnitMoved?.Invoke(this);
  }


  public void MoveTo(GameTile gameTile)
  {
    Debug.Log($"MoveTo: {name} at {Position} moving to {gameTile.name} at {gameTile.Position}");
    if (!GameWorld.ValidMoves(Position).Contains(gameTile)) return; //First see if this is a valid move
    Debug.Log("Valid Move");
    GameWorld.Tiles[Position].gameUnit = null; //First clear this unit off the current tile
    _target = gameTile.worldPosition;
    _position = gameTile.Position;
    GameWorld.Tiles[Position].gameUnit = this;//Then set this unit on the new tile
    OnGameUnitMoved?.Invoke(this);
  }

  public static void Train(string unitName, GameCity gameCity)
  {
    Debug.Log($"Train: {unitName}");
    Vector2Int position = gameCity.Position;
    // OnGameUnitTrained?.Invoke(this);
    GameObject gameUnitPrefab = Resources.Load<GameObject>($"Prefabs/{unitName}");
    GameObject gameUnitGameObject = GameObject.Instantiate(gameUnitPrefab);
    gameUnitGameObject.name = unitName;
    GameUnit gameUnit = gameUnitGameObject.GetComponent<GameUnit>();
    gameUnit.Position = position;
    GameItemSelector.Selected = gameUnit.gameObject;
    OnGameUnitTrained?.Invoke(gameUnit);
  }





  void Update()
  {
    float step = _speed * Time.deltaTime;
    transform.position = Vector3.MoveTowards(transform.position, _target, step);
  }

}
