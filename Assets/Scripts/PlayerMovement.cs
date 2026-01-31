using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalMoveSpeed;

    private InputActions input;
    private Rigidbody2D rb;
    private float horizontalMovement;

    private void Awake()
    {
        input = new InputActions();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontalMovement = 0f;
    }

    private void Update()
    {
        rb.linearVelocityX = horizontalMovement * horizontalMoveSpeed;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += Move;
        input.Player.Move.canceled += Move;
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= Move;
        input.Player.Move.canceled -= Move;
        input.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<float>();
    }
}
