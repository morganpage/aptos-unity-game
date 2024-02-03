using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
  [SerializeField] private GameObject _tutorialPanel;
  void Start()
  {
    Debug.Log("TestManager Start");
#if UNITY_EDITOR
    Debug.Log("UNITY_EDITOR");
    _tutorialPanel.SetActive(true);
#endif
  }

}
