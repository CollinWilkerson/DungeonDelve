using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//A LARGE PORTION OF THIS CONTROLLER ORIGINALLY COMES FROM SPENCER GALLON, I TAKE NO CREDIT FOR HIS WORK: https://github.com/CollinWilkerson/SciFiFarming/blob/Multiplayer/SciFiFarming/Assets/Scripts/PlayerController.cs
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveLerpRate = 0.5f;

    [Header("Jump")]
    [SerializeField] private float groundedCheckDistance = 0.6f;
    [SerializeField] private float jumpForce = 20f;
    private bool tryJump = false;

    [Header("Camera Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float fieldOfView = 60f;

    private Camera playerCamera;
    private float xRotation = 0f;
    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");

        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = fieldOfView;
        }
        else
        {
            Debug.LogError("NO CAMERA ON PLAYER!");
        }

        Cursor.lockState = CursorLockMode.Locked;

        #if UNITY_EDITOR
            mouseSensitivity *= 100;
        #endif
    }

    private void Update()
    {

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            HandleMovement();
            HandleLook();
        }

        if (Physics.Raycast(transform.position, Vector3.down, groundedCheckDistance) && jumpAction.IsPressed())
        {
            //Debug.Log("tryjump");
            tryJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (tryJump)
        {
            HandleJump();
            tryJump = false;
        }
    }

    private void HandleMovement()
    {
        float moveHorizontal = moveAction.ReadValue<Vector2>().x;
        float moveVertical = moveAction.ReadValue<Vector2>().y;

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        Vector3 targetVelocity = movement.normalized * moveSpeed;
        rb.linearVelocity = Vector3.Lerp( rb.linearVelocity, new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z), moveLerpRate * Time.deltaTime);
    }

    private void HandleLook()
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

    private void HandleJump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
}
