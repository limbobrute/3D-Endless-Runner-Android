using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private TouchControl controls;
    private Camera MainCam;
    #region EVENTS
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(this); }
        else
        { Instance = this; }

        controls = new TouchControl();
        MainCam = Camera.main;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        controls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if(OnStartTouch != null)
        { OnStartTouch(Utils.ScreenToWorld(MainCam, controls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime); }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        { OnEndTouch(Utils.ScreenToWorld(MainCam, controls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time); }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(MainCam, controls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
