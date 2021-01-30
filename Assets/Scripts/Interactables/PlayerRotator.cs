using UnityEngine;
using WebXR;

public class PlayerRotator : MonoBehaviour
{
  public WebXRController controller = null;
  public Transform bodyTransform = null;
  public Transform offsetOriginTransform = null;
  public float minAxisX = 0.5f;
  public float rotationStep = 45f;

  private float timeRotatePlayer = -1f;
  private bool axisNotZero = false;
  private bool canRotate = false;
  private float currentRotation = 0;

  private void Update()
  {
    if (!controller.isControllerActive)
    {
      timeRotatePlayer = -1f;
      return;
    }

    Vector2 thumbstick = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
    Vector2 touchpad = controller.GetAxis2D(WebXRController.Axis2DTypes.Touchpad);

    axisNotZero = Mathf.Abs(thumbstick.x) > minAxisX || Mathf.Abs(touchpad.x) > minAxisX;
    if (timeRotatePlayer < 0 && axisNotZero)
    {
      timeRotatePlayer = Time.time;
    }
    canRotate = timeRotatePlayer > 0 && Time.time >= timeRotatePlayer;
    if (axisNotZero)
    {
      if (Mathf.Abs(thumbstick.x) > minAxisX)
      {
        currentRotation = thumbstick.x > 0 ? rotationStep : - rotationStep;
      }
      else
      {
        currentRotation = touchpad.x > 0 ? rotationStep : - rotationStep;
      }
    }

    if (!axisNotZero)
    {
      if (canRotate)
      {
        bodyTransform.RotateAround(offsetOriginTransform.position, Vector3.up, currentRotation);
      }
      timeRotatePlayer = -1f;
    }
  }
}
