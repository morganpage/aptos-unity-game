using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tweener : MonoBehaviour
{
  public Action OnTweenComplete;
  [SerializeField] private Transform start;
  [SerializeField] private Transform end;
  [SerializeField] private GameObject objectToTween;
  [SerializeField] private float _speed = 1.0f;
  public int _times = 0;
  public Vector3 _endPosition;

  void Awake()
  {
    objectToTween.SetActive(false);
  }

  [ContextMenu("TestTween")]
  private void TestTween()
  {
    StartTween(5);
  }

  public void StartTween(int numberOfTimes)
  {
    Debug.Log("StartTween");
    _times = numberOfTimes;
    Reset();
  }

  void Update()
  {
    if (_times <= 0) return;
    float step = _speed * Time.deltaTime;
    objectToTween.transform.position = Vector3.MoveTowards(objectToTween.transform.position, _endPosition, step);
    float distance = Vector3.Distance(objectToTween.transform.position, _endPosition);
    //Debug.Log($"Tweening {distance} {objectToTween.transform.position} {_endPosition.x} {_endPosition.y} ");
    if (distance < 0.01f)
    {
      _times--;
      Reset();
      OnTweenComplete?.Invoke();
      if (_times == 0)
      {
        objectToTween.SetActive(false);
      }
    }
  }

  void Reset()
  {
    objectToTween.SetActive(true);
    Vector3 startPoint = GetScreenPosition(start);
    startPoint.z = 0;
    objectToTween.transform.position = startPoint;
    _endPosition = GetScreenPosition(end);
    _endPosition.z = 0;
  }

  private Vector3 GetScreenPosition(Transform point)
  {
    RectTransform rt = point.gameObject.GetComponent<RectTransform>();
    if (rt != null)
    {
      return rt.position;
    }
    else
    {
      return Camera.main.WorldToScreenPoint(point.position);
    }
  }


}
