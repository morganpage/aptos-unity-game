using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInfluence : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _influenceText;

  void OnEnable()
  {
    GameWorld.OnInfluenceChanged += OnInfluenceChanged;
  }

  void OnDisable()
  {
    GameWorld.OnInfluenceChanged -= OnInfluenceChanged;
  }

  void OnInfluenceChanged(int influence)
  {
    _influenceText.text = influence.ToString();
  }

}
