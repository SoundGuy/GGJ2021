using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR || !UNITY_WEBGL
using UnityEngine.XR;
using UnityEngine.XR.Management;
#endif
using WebXR;

public class FlatController : MonoBehaviour
{
  public float speed = 10f;
  public float mouseSensitivity = 1f;

  [SerializeField]
  private Camera flatCamera = null;
  [SerializeField]
  private Camera pointerCamera = null;
  [SerializeField]
  private Camera arLeftCamera = null;
  [SerializeField]
  private StandaloneInputModule standaloneInputModule;
  [SerializeField]
  private ControllerInputModule controllerInputModule;
  [SerializeField]
  private Canvas timeCanvas2D = null;

  private Transform _transform;
  private Transform flatCameraTransform;

  private float minimumX = -360f;
  private float maximumX = 360f;

  private float minimumY = -90f;
  private float maximumY = 90f;

  private float rotationX = 0f;
  private float rotationY = 0f;

  private Quaternion originalRotation;
  private Quaternion flatCameraOrigin;

  private Vector3 axis;
  private Vector3 axisLastFrame;
  private Vector3 axisDelta;

  private float forward = 0;
  private float sides = 0;

  private bool mainCameraIsFlat = true;

  void Awake()
  {
    _transform = transform;
    flatCameraTransform = flatCamera.transform;
    if (standaloneInputModule == null)
    {
      standaloneInputModule = FindObjectOfType<StandaloneInputModule>();
    }
    if (controllerInputModule == null)
    {
      controllerInputModule = FindObjectOfType<ControllerInputModule>();
    }
  }

#if UNITY_EDITOR || !UNITY_WEBGL
  void Start()
  {
    var xrSettings = XRGeneralSettings.Instance;
    if (xrSettings == null)
    {
      Debug.Log($"XRGeneralSettings is null.");
      SetControllerInput(false);
      SetCanvas2D(flatCamera);
      return;
    }

    var xrManager = xrSettings.Manager;
    if (xrManager == null)
    {
      Debug.Log($"XRManagerSettings is null.");
      SetControllerInput(false);
      SetCanvas2D(flatCamera);
      return;
    }

    var xrLoader = xrManager.activeLoader;
    if (xrLoader == null)
    {
      Debug.Log($"XRLoader is null.");
      SetControllerInput(false);
      SetCanvas2D(flatCamera);
      return;
    }

    var xrInput = xrLoader.GetLoadedSubsystem<XRInputSubsystem>();

    if (xrInput == null)
    {
      Debug.Log($"XRInput is null.");
      SetControllerInput(false);
      SetCanvas2D(flatCamera);
      return;
    }

    mainCameraIsFlat = false;
    SetControllerInput(true);
    SetCanvas2D(null);
  }
#else
  void Start()
  {
    SetControllerInput(false);
    SetCanvas2D(flatCamera);
  }
#endif

  void OnEnable()
  {
    ResetValues();
    WebXRManager.OnXRChange += OnXRChange;
  }

  void OnDisable()
  {
    WebXRManager.OnXRChange -= OnXRChange;
  }

  private void OnXRChange(WebXRState state, int viewsCount, Rect leftRect, Rect rightRect)
  {
    switch (state)
    {
      case WebXRState.NORMAL:
        ResetValues();
        SetControllerInput(false);
        SetCanvas2D(flatCamera);
        break;
      case WebXRState.VR:
        SetControllerInput(true);
        SetCanvas2D(null);
        break;
      case WebXRState.AR:
        SetControllerInput(viewsCount == 2);
        SetCanvas2D(viewsCount == 2 ? null : arLeftCamera);
        break;
    }
  }

  private void SetCanvas2D(Camera camera)
  {
    if (camera != null)
    {
      timeCanvas2D.worldCamera = camera;
      timeCanvas2D.gameObject.SetActive(true);
    }
    else
    {
      timeCanvas2D.gameObject.SetActive(false);
    }
  }

  public void ResetValues()
  {
    originalRotation = _transform.localRotation;
    flatCameraOrigin = flatCameraTransform.localRotation;
    rotationX = 0f;
    rotationY = 0f;
  }

  void SetControllerInput(bool active)
  {
    if (controllerInputModule != null && standaloneInputModule != null)
    {
      controllerInputModule.enabled = active;
      standaloneInputModule.enabled = !active;
      UpdateCanvases(active ? pointerCamera : flatCamera);
    }
  }

  void UpdateCanvases(Camera camera)
  {
    var canvases = FindObjectsOfType<Canvas>(true);
    foreach (var canvas in canvases)
    {
      if (canvas != timeCanvas2D)
      {
        canvas.worldCamera = camera;
      }
    }
  }

  void Update()
  {
    if (!mainCameraIsFlat || !flatCamera.enabled)
    {
      return;
    }
    if (Input.GetMouseButtonDown(0))
    {
      axisLastFrame = flatCamera.ScreenToViewportPoint(Input.mousePosition);
    }
    if (Input.GetMouseButton(0))
    {
      axis = flatCamera.ScreenToViewportPoint(Input.mousePosition);
      axisDelta = (axisLastFrame - axis) * 90f;
      axisLastFrame = axis;

      rotationX += axisDelta.x * mouseSensitivity;
      rotationY += axisDelta.y * mouseSensitivity;

      rotationX = ClampAngle(rotationX, minimumX, maximumX);
      rotationY = ClampAngle(rotationY, minimumY, maximumY);

      Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
      Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

      _transform.localRotation = originalRotation * xQuaternion;
      flatCameraTransform.localRotation = flatCameraOrigin * yQuaternion;
    }
    forward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
    sides = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    transform.Translate(sides, 0, forward);
  }

  private float ClampAngle(float angle, float min, float max)
  {
    if (angle < -360f)
      angle += 360f;
    if (angle > 360f)
      angle -= 360f;
    return Mathf.Clamp(angle, min, max);
  }
}
