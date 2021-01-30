using UnityEngine;
using WebXR;

public class ControllerSelector : MonoBehaviour
{
  public WebXRController controllerL = null;
  public WebXRController controllerR = null;
  public GameObject pointerGameObject;

  private Transform leftTransform;
  private Transform rightTransform;
  private Transform _transform;

  private ControllerInputModule controllerInputModule;

  private void Awake()
  {
    _transform = transform;
    leftTransform = controllerL.transform;
    rightTransform = controllerR.transform;
  }

  private void Start()
  {
    controllerInputModule = FindObjectOfType<ControllerInputModule>();
  }

  private void Update()
  {
    if (!controllerL.isControllerActive && !controllerR.isControllerActive)
    {
      SetPointerActive(false);
    }
    else if (!controllerL.isControllerActive)
    {
      if (SetParent(rightTransform))
      {
        SetControllerInput(controllerR);
      }
      SetPointerActive(true);
      return;
    }
    else if (!controllerR.isControllerActive)
    {
      if (SetParent(leftTransform))
      {
        SetControllerInput(controllerL);
      }
      SetPointerActive(true);
      return;
    }
    else if (controllerL.isControllerActive && controllerR.isControllerActive)
    {
      if (controllerR.GetButtonDown(WebXRController.ButtonTypes.Trigger))
      {
        if (SetParent(rightTransform))
        {
          SetControllerInput(controllerR);
        }
      }
      else if (controllerL.GetButtonDown(WebXRController.ButtonTypes.Trigger))
      {
        if (SetParent(leftTransform))
        {
          SetControllerInput(controllerL);
        }
      }
      SetPointerActive(true);
    }
  }

  private bool SetParent(Transform parent)
  {
    if (_transform.parent != parent)
    {
      _transform.SetParent(parent);
      _transform.localPosition = Vector3.zero;
      _transform.localRotation = Quaternion.identity;
      _transform.localScale = Vector3.one;
      return true;
    }
    return false;
  }

  private void SetPointerActive(bool active)
  {
    if (pointerGameObject.activeSelf != active)
    {
      pointerGameObject.SetActive(active);
    }
  }

  private void SetControllerInput(WebXRController controller)
  {
    controllerInputModule.controller = controller;
  }
}
