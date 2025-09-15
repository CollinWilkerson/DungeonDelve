using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//A LARGE PORTION OF THIS CONTROLLER ORIGINALLY COMES FROM SPENCER GALLOLN, I TAKE NO CREDIT FOR HIS WORK: https://github.com/CollinWilkerson/SciFiFarming/blob/Multiplayer/SciFiFarming/Assets/Scripts/PlayerController.cs
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("Camera Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float fieldOfView = 60f;

    private Camera playerCamera;
    private float xRotation = 0f;
    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction lookAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = fieldOfView;
        }
        else
        {
            Debug.LogError("NO CAMERA ON PLAYER!");
        }

    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        float moveHorizontal = moveAction.ReadValue<Vector2>().x;
        float moveVertical = moveAction.ReadValue<Vector2>().y;

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        Vector3 targetVelocity = movement.normalized * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    void HandleLook()
    {
        float mouseX = lookAction.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookAction.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        transform.Rotate(Vector3.up * mouseX);
    }
}
