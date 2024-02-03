using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSpriteSwitch : MonoBehaviour, IOutline
{
  [SerializeField] private bool _outline;
  private SpriteRenderer _spriteRenderer;

  public bool Outline
  {
    get => _outline;
    set
    {
      _outline = value;
      _spriteRenderer.sprite = Resources.Load<Sprite>(_outline ? $"Sprites/Outline/{this.name}" : $"Sprites/NoOutline/{this.name}");
    }
  }

  void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    Outline = _outline;
  }

  [ContextMenu("Toggle Outline")]
  public void ToggleOutline()
  {
    Outline = !Outline;
  }

}
