// Controller Input Module by De-Panther
// Based on Peter Koch <peterept@gmail.com> Gaze Input Module
// http://talesfromtherift.com/vr-gaze-input/

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using WebXR;

public class ControllerInputModule : PointerInputModule
{
  public WebXRController controller;
  public Transform hitPoint;
  public GameObject hitPointGameObject;

  private PointerEventData pointerEventData;
  private GameObject currentPointAtHandler;

  protected override void OnDisable()
  {
    base.OnDisable();
    hitPointGameObject.SetActive(false);
  }

  public override void Process()
  {
    HandlePoint();
    HandleSelection();
  }

  void HandlePoint()
  {
    if (pointerEventData == null)
    {
      pointerEventData = new PointerEventData(eventSystem);
    }
    pointerEventData.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    pointerEventData.delta = Vector2.zero;
    List<RaycastResult> raycastResults = new List<RaycastResult>();
    eventSystem.RaycastAll(pointerEventData, raycastResults);
    pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults);
    hitPointGameObject.SetActive(pointerEventData.pointerCurrentRaycast.isValid);
    if (pointerEventData.pointerCurrentRaycast.isValid)
    {
      hitPoint.position = pointerEventData.pointerCurrentRaycast.worldPosition;
    }
    ProcessMove(pointerEventData);
  }

  void HandleSelection()
  {
    if (pointerEventData.pointerEnter != null)
    {
      GameObject handler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointerEventData.pointerEnter);
      if (currentPointAtHandler != handler)
      {
        currentPointAtHandler = handler;
      }

      if (currentPointAtHandler != null && controller.GetButtonUp(WebXRController.ButtonTypes.Trigger))
      {
        ExecuteEvents.ExecuteHierarchy(currentPointAtHandler, pointerEventData, ExecuteEvents.pointerClickHandler);
      }
    }
    else
    {
      currentPointAtHandler = null;
    }
  }


}
