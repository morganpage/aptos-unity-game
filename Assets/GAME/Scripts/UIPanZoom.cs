using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanZoom : MonoBehaviour
{
  [SerializeField] private float _zoomSpeed = 1;
  [SerializeField] private Transform _transformCamera;
  private Vector3 _origin;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    HandlePan();
    HandleZoom();
  }


  void HandlePan()
  {
    //Pan the camera with the middle mouse button
    if (Input.GetMouseButtonDown(2))
    {
      _origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    if (Input.GetMouseButton(2))
    {
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3 difference = worldPosition - _transformCamera.position;
      _transformCamera.position = _origin - difference;
    }
  }

  void HandleZoom()
  {
    //Zoom the camera with the mouse wheel
    float scrollData = Input.GetAxis("Mouse ScrollWheel");
    if (scrollData != 0)
    {
      Camera camera = _transformCamera.GetComponent<Camera>();
      camera.orthographicSize -= scrollData * _zoomSpeed;
    }
  }
}
