using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private SpriteRenderer maskSr; 

    private InputActions input;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    private float horizontalMovement;

    private void Awake()
    {
        input = new InputActions();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if (horizontalMovement > 0)
        {
            sr.flipX = false;
            maskSr.flipX = false; 
        }
        else if (horizontalMovement < 0)
        {
            sr.flipX = true;
            maskSr.flipX = true; 
        }
    }
}
