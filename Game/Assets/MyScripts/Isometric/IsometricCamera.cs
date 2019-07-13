using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
  public Transform target;
  public float zoomSpeed = 10f;
  public float maxZoom = 5f;
  public float rotationSpeed = 10f;
  public float maxRotation = 50f;
  public float panningSpeed = 10f;
  public Vector2 panninglimits = Vector2.zero;
  public float overviewDistance = 20f;
  public float overviewTouchDrag = 30f;
  public float smoothness = 0.5f;

  private bool _overviewOn;
  private bool _useRotation;
  private bool _useZoom;
  private float _xAxis;
  private float _yAxis;
  private float _zoom;
  private float _baseZoom;
  private float _rotation;
  private float _rotationTimer;
  private float _overviewDrag;
  private float _touchDistance;
  private float _touchTolerance;
  private Touch _touchOne;
  private Touch _touchTwo;
  private Vector2 _touchOneDelta;
  private Vector3 _viewPoint;
  private Vector3 _wrapperVelocity = Vector3.zero;
  private Vector3 _cameraVelocity = Vector3.zero;
  private Quaternion _localCameraRotation;
  private Transform _cameraWrapper;

  private void Start()
  {
    _rotationTimer = 0f;
    _touchTolerance = Screen.width * 0.02f;
    _overviewDrag = Screen.width * (overviewTouchDrag / 100);
    // We start up looking at the center of our target
    transform.LookAt(target);
    // Create a wrapper for simple pivoting rotations
    GameObject wrapper = new GameObject();
    wrapper.name = "Camera Wrapper";
    _cameraWrapper = wrapper.transform;
    _cameraWrapper.SetPositionAndRotation(target.position, Quaternion.identity);
    transform.parent = _cameraWrapper;
    _localCameraRotation = transform.localRotation;
    _zoom = _baseZoom = Vector3.Distance(transform.position, target.position);
    // Initiate a view point inside panning limits
    SetViewPoint(target.position);
  }

  private void Update()
  {
    // Get all touch input and values before anything
    SetInputValues();
    // If we are not overviewing, we apply panning, zoom or rotate
    if (!_overviewOn)
    {
      GetPanningValue();
      GetZoomValue();
      RotateWrapper();
    }
    // Resulting modifier values are applied here.
    MoveCameraAndWrapper();
    RotateCamera();
  }

  private void SetInputValues()
  {
    ResetValues();

    if (Input.touchCount > 0)
    {
      _touchOne = Input.GetTouch(0);

      if (_touchOne.phase == TouchPhase.Moved)
      {
        _touchOneDelta += _touchOne.deltaPosition;
      }
      else if (_touchOne.phase == TouchPhase.Ended)
      {
        ResetValues(true);
        return;
      }

      if (Input.touchCount == 1)
      {
        if (Mathf.Abs(_touchOneDelta.x) > Mathf.Abs(_touchOneDelta.y))
        {
          _useRotation = true;
        }
        else if (Mathf.Abs(_touchOneDelta.y) >= _overviewDrag)
        {
          _overviewOn = true;
        }
      }
      else if (Input.touchCount == 2)
      {
        _touchTwo = Input.GetTouch(1);
        float currentDistance = Vector2.Distance(_touchOne.position, _touchTwo.position);

        if (_touchTwo.phase == TouchPhase.Began)
        {
          _touchDistance = currentDistance + _touchTolerance;
        }
        else if (_touchTwo.phase == TouchPhase.Moved)
        {
          if (_touchOne.phase == TouchPhase.Moved && _touchDistance > currentDistance)
          {
            Vector2 midPoint = ((_touchOne.deltaPosition + _touchTwo.deltaPosition) / 2f).normalized;
            _xAxis = -midPoint.x;
            _yAxis = -midPoint.y;
          }
        }
        else if (_touchTwo.phase == TouchPhase.Ended)
        {
          ResetValues(true);
          return;
        }

        if (currentDistance > _touchDistance + _touchTolerance * 3f)
        {
          _useZoom = true;
        }
      }
    }
    else
    {
      ResetValues(true);
    }
  }

  // Reset input variables
  private void ResetValues(bool clearDeltas = false)
  {
    _overviewOn = false;
    _useZoom = false;
    _useRotation = false;
    _xAxis = 0f;
    _yAxis = 0f;

    if (clearDeltas)
    {
      _touchOneDelta = Vector2.zero;
      _touchDistance = 0f;
    }
  }

  // For public usage. Checks if provided v3 coords are 
  // inside allowed limits, if not, it clamps it.
  public void SetViewPoint(Vector3 newViewPoint)
  {
    newViewPoint.x = Mathf.Clamp(newViewPoint.x, -panninglimits.x, panninglimits.x);
    newViewPoint.z = Mathf.Clamp(newViewPoint.z, -panninglimits.y, panninglimits.y);
    _viewPoint = newViewPoint;
  }

  // 2Touch (average) drag
  // TODO: Paneo se siente culero, arreglar please.
  private void GetPanningValue()
  {
    if (!_useZoom && (Mathf.Abs(_xAxis) > 0f || Mathf.Abs(_yAxis) > 0f))
    {
      Vector2 input = new Vector2(_xAxis, _yAxis);
      input = Vector2.ClampMagnitude(input, 1);

      Vector3 foward = transform.forward;
      Vector3 right = transform.right;

      foward.y = 0f;
      right.y = 0f;
      foward = foward.normalized;
      right = right.normalized;

      // TODO: Arreglar esto!!!!!
      Vector3 newPosition = foward * input.y + right * input.x;

      SetViewPoint(_viewPoint + newPosition * Time.deltaTime * panningSpeed);
    }
  }

  // 2Touch difference
  private void GetZoomValue()
  {
    bool applyZoom = true;
    float distance = Vector3.Distance(transform.position, _cameraWrapper.position);

    if (_useZoom)
    {
      if (distance > maxZoom)
      {
        _zoom -= Time.deltaTime * zoomSpeed;
      }
      else
      {
        applyZoom = false;
      }
    }
    else if (distance < _baseZoom)
    {
      _zoom += Time.deltaTime * zoomSpeed;
    }
    else
    {
      applyZoom = false;
    }

    if (applyZoom)
    {
      _zoom = Mathf.Clamp(_zoom, maxZoom, _baseZoom);
    }
  }

  // 1Touch Horizontal drag
  private void RotateWrapper()
  {
    bool applyRotation = true;
    if (_useRotation)
    {
      _rotation += _touchOne.deltaPosition.x * rotationSpeed * Time.deltaTime;
      _rotation = Mathf.Clamp(_rotation, -maxRotation, maxRotation);
    }
    else if (_rotation != 0)
    {
      _rotation = Mathf.Lerp(_rotation, 0f, _rotationTimer);
      _rotationTimer += Time.deltaTime * 2f;

      if (Mathf.Round(_rotation) == 0)
      {
        _rotationTimer = 0;
        _rotation = 0;
      }
    }
    else
    {
      applyRotation = false;
    }

    if (applyRotation)
    {
      Vector3 currentRotation = _cameraWrapper.eulerAngles;
      currentRotation.y = _rotation;
      _cameraWrapper.rotation = Quaternion.Euler(currentRotation);
    }
  }

  private void MoveCameraAndWrapper()
  {
    Vector3 newPosition = Vector3.zero;

    if (_overviewOn)
    {
      transform.position = Vector3.SmoothDamp(transform.position, target.position + target.up * overviewDistance, ref _cameraVelocity, smoothness);
    }
    else
    {
      transform.position = Vector3.SmoothDamp(transform.position, _cameraWrapper.position - transform.forward * _zoom, ref _cameraVelocity, smoothness);
    }

    _cameraWrapper.position = Vector3.SmoothDamp(_cameraWrapper.position, _viewPoint, ref _wrapperVelocity, smoothness);
  }

  // TODO: Intentar hacer que la camara no muestra angulos 
  // muertos durante las transiciones... lograr un
  // LookAt entre el centro del target y la rotacion
  // original de la camara relativo a su wrapper.
  private void RotateCamera()
  {
    if (_overviewOn)
    {
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * rotationSpeed);
    }
    else
    {
      transform.localRotation = Quaternion.Slerp(transform.localRotation, _localCameraRotation, Time.deltaTime * rotationSpeed);
    }
  }

  #region DEBUGGING
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.blue;
    if (_overviewOn)
    {
      Gizmos.DrawLine(transform.position, target.position);
    }
    else
    {
      Gizmos.DrawLine(transform.position, _viewPoint);
    }

    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(_viewPoint, 0.18f);

    Gizmos.color = Color.green;
    Vector3 a = new Vector3(-panninglimits.x, 0.18f, -panninglimits.y);
    Vector3 b = new Vector3(panninglimits.x, 0.18f, -panninglimits.y);
    Gizmos.DrawLine(a, b);
    a.x = panninglimits.x;
    a.z = panninglimits.y;
    Gizmos.DrawLine(b, a);
    b.x = -panninglimits.x;
    b.z = panninglimits.y;
    Gizmos.DrawLine(a, b);
    a.x = -panninglimits.x;
    a.z = -panninglimits.y;
    Gizmos.DrawLine(b, a);
  }
  #endregion
}
