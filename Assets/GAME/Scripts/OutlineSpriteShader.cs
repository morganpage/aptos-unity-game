using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IOutline { bool Outline { get; set; } }

public class OutlineSpriteShader : MonoBehaviour, IOutline
{
  [SerializeField] private float _outlineWidth = 0.1f;
  [SerializeField] private bool _outline;
  private int shaderOffset = Shader.PropertyToID("_Offset");
  private SpriteRenderer _spriteRenderer;

  public bool Outline
  {
    get => _outline;
    set
    {
      _outline = value;
      float offset = 1.0f;
      if (_outline)
      {
        offset += _outlineWidth;
      }
      _spriteRenderer.material.SetFloat(shaderOffset, offset);
      transform.localScale = new Vector3(offset, offset, 1);
    }
  }

  void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    Outline = false;
  }

  [ContextMenu("Toggle Outline")]
  public void ToggleOutline()
  {
    Outline = !Outline;
  }

}
