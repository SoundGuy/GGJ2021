using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLRotator : MonoBehaviour
{
  #if UNITY_WEBGL && !UNITY_EDITOR
  private void Awake()
  {
    transform.Rotate(90f,0,0);
  }
  #endif
}
