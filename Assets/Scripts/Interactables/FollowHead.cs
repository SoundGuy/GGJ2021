using UnityEngine;
using WebXR;

public class FollowHead : MonoBehaviour
{
  [SerializeField]
  private WebXRCamera webXRCamera = null;

  private Transform _transform = null;

  private void Awake()
  {
    _transform = transform;
  }

  private void Update()
  {
    _transform.localPosition = webXRCamera.GetLocalPosition();
    _transform.localRotation = webXRCamera.GetLocalRotation();
  }
}
