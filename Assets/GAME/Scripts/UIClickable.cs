using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIClickable : MonoBehaviour, IPointerDownHandler
{
  public UnityEvent HandlePointerDown;

  public void OnPointerDown(PointerEventData eventData)
  {
    HandlePointerDown?.Invoke();
  }

}