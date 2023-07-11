using UnityEngine;
using UnityEngine.InputSystem;

public class CursorScript : MonoBehaviour
{
    public BehaviorSwapper swapper;

    [SerializeField]
    public InputActionReference leftClick, rightClick;

    private Camera mainCam;
    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cursor = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector2(cursor.x, cursor.y);
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        swapper.OnLeftClick(context);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        swapper.OnRightClick(context);
    }
}
