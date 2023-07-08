using UnityEngine;
using UnityEngine.InputSystem;

public class CursorScript : MonoBehaviour
{

    [SerializeField]
    private InputActionReference leftClick, rightClick;

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

}
