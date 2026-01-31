using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentTarget;
    private InputActions input;

    private void Awake()
    {
        input = new InputActions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Interact.performed += Interact;
    }

    private void OnDisable()
    {
        input.Player.Interact.performed -= Interact;
        input.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            currentTarget = interactable;
            interactable.OnEnterRange();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (currentTarget == interactable)
            {
                interactable.OnExitRange();
                currentTarget = null;
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        currentTarget?.OnInteract();
    }
}
