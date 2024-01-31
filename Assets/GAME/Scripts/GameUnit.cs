using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameUnit : MonoBehaviour
{
  public static Action<GameUnit> OnGameUnitSelected;
  public static Action<GameUnit> OnGameUnitMoved;
  public Vector2Int position;
  private Vector2 _target;
  private IOutline _outlineSprite;
  private float _speed = 5f;
  private static GameUnit _selected;
  public static GameUnit Selected
  {
    get => _selected;
    set
    {
      if (_selected != null) _selected.GetComponent<IOutline>().Outline = false;//First un select the currently selected unit
      _selected = value;
      if (_selected != null) _selected.GetComponent<IOutline>().Outline = true;//Then select the newly selected unit
      OnGameUnitSelected?.Invoke(_selected);
    }
  }

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
    Debug.Log($"MoveTo: {name} at {position} moving to {gameTile.name} at {gameTile.position}");
    if (!GameWorld.ValidMoves(position).Contains(gameTile)) return; //First see if this is a valid move
    GameWorld.Tiles[position].gameUnit = null; //First clear this unit off the current tile
    _target = gameTile.worldPosition;
    position = gameTile.position;
    GameWorld.Tiles[position].gameUnit = this;//Then set this unit on the new tile
    OnGameUnitMoved?.Invoke(this);
  }

  void Update()
  {
    float step = _speed * Time.deltaTime;
    transform.position = Vector3.MoveTowards(transform.position, _target, step);
  }

}
