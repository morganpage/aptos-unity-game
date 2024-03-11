using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameInfo : MonoBehaviour
{
  [SerializeField] private Image _image;
  [SerializeField] private TextMeshProUGUI _textTitle;
  [SerializeField] private TextMeshProUGUI _textDescription;
  [SerializeField] private GameObject _panel;
  [SerializeField] private Button[] _buttonChoices;

  void Awake()
  {
    ClearChoices();
  }

  public void SetInfo(string title, string description, string spriteName = "UI/Rogue")
  {
    _textTitle.text = title;
    _textDescription.text = description;
    _image.sprite = Resources.Load<Sprite>($"Sprites/{spriteName}");
  }

  public void Show(bool show)
  {
    _panel.SetActive(show);
  }

  public void ClearChoices()
  {
    foreach (Button button in _buttonChoices)
    {
      button.gameObject.SetActive(false);
      button.onClick.RemoveAllListeners();
    }
  }

  public void AddChoice(string spriteName, string text, System.Action action)
  {
    Debug.Log("AddChoice");
    foreach (Button button in _buttonChoices)
    {
      if (button.gameObject.activeSelf == false)
      {
        button.gameObject.SetActive(true);
        button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        GetButtonIconImage(button).sprite = Resources.Load<Sprite>("Sprites/UI/" + spriteName);
        button.onClick.AddListener(() => action());
        return;
      }
    }

  }

  private Image GetButtonIconImage(Button button)
  {
    Image[] images = button.GetComponentsInChildren<Image>();
    foreach (Image image in images)
    {
      if (image.gameObject.name == "ImageIcon")
      {
        return image;
      }
    }
    return null;
  }


}
