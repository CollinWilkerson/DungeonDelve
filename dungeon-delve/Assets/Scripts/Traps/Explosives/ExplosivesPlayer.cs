using UnityEngine;
using UnityEngine.InputSystem;

public class ExplosivesPlayer : MonoBehaviour
{
    [Header("Control Sensitivity")]
    [SerializeField] private float groundedCheckDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveModifier;

    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction jumpAction;

    private bool tryJump = false;

    // Update is called once per frame
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if(transform.position.y < 0)
        {
            FindAnyObjectByType<TrapBase>().Fail();
        }

        bool isgrounded = Physics.Raycast(gameObject.transform.position,
            Vector3.down, groundedCheckDistance);

        if(jumpAction.triggered && isgrounded)
        {
            tryJump = true;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 100, ForceMode.Acceleration);
        float horizontalInput = moveAction.ReadValue<Vector2>().x;
        rb.AddForce(Vector3.right * horizontalInput * moveModifier, ForceMode.VelocityChange);
        float verticalInput = moveAction.ReadValue<Vector2>().y;
        rb.AddForce(Vector3.forward * verticalInput * moveModifier, ForceMode.VelocityChange);
        if (tryJump)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            tryJump = false;
        }
    }
}
