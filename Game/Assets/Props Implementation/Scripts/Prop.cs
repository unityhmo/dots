using UnityEngine;

public class Prop : MonoBehaviour
{
  enum PropType
  {
    Clan = 0,
    Quad = 1
  }

  [SerializeField]
  private PropType _propType = PropType.Clan;
  public Transform animatedContentWrapper;
  [SerializeField]
  private float _managerCallbackTimer = 0.85f;
  [SerializeField]
  private float _autoDestroyTimer = 1.01f;
  private Animator _animator;
  private PropsManager _manager;

  private void Awake()
  {
    _animator = animatedContentWrapper.GetComponent<Animator>();
    animatedContentWrapper.gameObject.SetActive(false);
  }

  public Transform StartUp(PropsManager manager)
  {
    _manager = manager;

    animatedContentWrapper.gameObject.SetActive(true);
    if (_animator)
    {
      _animator.Play("go");
    }

    Invoke("ManagerCallback", _managerCallbackTimer);
    Invoke("AutoDestroy", _autoDestroyTimer);

    return transform;
  }

  private void ManagerCallback()
  {
    if (_propType == PropType.Clan)
    {
      _manager.InstantiateCapturedQuad(transform.position);
    }
    else
    {
      _manager.QuadCaptureAnimationCompleted(transform.position);
    }
  }

  private void AutoDestroy()
  {
    Destroy(gameObject);
  }
}
