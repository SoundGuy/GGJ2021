using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
  public int layerIndex = 10;

  public UnityEvent triggerEnter;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == layerIndex)
    {
      triggerEnter.Invoke();
    }
  }
}
