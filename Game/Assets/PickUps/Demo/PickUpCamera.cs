using UnityEngine;

public class PickUpCamera : MonoBehaviour
{
  [SerializeField]
  private float _rotationSpeed = 10f;
  [SerializeField]
  private float _maxRotation = 50f;
  private float _rotationAngle;
  private float _currentMousePosition;
  private Vector3 _rotation;
  private Touch _touch;

  private void Start()
  {
    _rotationAngle = transform.eulerAngles.y;
    _rotation = new Vector3(0f, _rotationAngle, 0f);
  }

  private void Update()
  {
    if (Input.GetMouseButton(0))
    {
      if (Input.GetMouseButtonDown(0))
        _currentMousePosition = Input.mousePosition.x;
      else if (_currentMousePosition != Input.mousePosition.x)
      {
        RotateTransform(_currentMousePosition - Input.mousePosition.x);
        _currentMousePosition = Input.mousePosition.x;
        return;
      }
    }
    else if (Input.touchCount > 0)
    {
      _touch = Input.GetTouch(0);

      if (_touch.phase == TouchPhase.Moved)
        RotateTransform(_touch.deltaPosition.x);
    }
    else if (_rotationAngle != 0f)
    {
      RotateTransform(_rotationAngle - Mathf.Lerp(_rotationAngle, 0, Time.deltaTime * _rotationSpeed), false);
    }
  }

  private void RotateTransform(float rotationIncrement, bool applyTimeScale = true)
  {
    if (applyTimeScale)
    {
      _rotationAngle -= rotationIncrement * Time.deltaTime * _rotationSpeed;
    }
    else
    {
      _rotationAngle -= rotationIncrement;
    }

    _rotationAngle = Mathf.Clamp(_rotationAngle, -_maxRotation, _maxRotation);
    _rotation.y = _rotationAngle;
    transform.rotation = Quaternion.Euler(_rotation);
  }
}
