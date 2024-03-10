using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
  [SerializeField] private UIGameInfo _uiGameInfo;
  void Start()
  {
    Debug.Log("TestManager Start");
#if UNITY_EDITOR
    Debug.Log("UNITY_EDITOR");
    _uiGameInfo.Show(true);
#endif
  }

}
