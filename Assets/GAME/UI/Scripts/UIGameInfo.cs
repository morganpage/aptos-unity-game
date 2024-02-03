using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameInfo : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _textTitle;
  [SerializeField] private TextMeshProUGUI _textDescription;
  [SerializeField] private GameObject _panel;

  public void SetInfo(string title, string description)
  {
    _textTitle.text = title;
    _textDescription.text = description;
  }

  public void Show(bool show)
  {
    _panel.SetActive(show);
  }

}
