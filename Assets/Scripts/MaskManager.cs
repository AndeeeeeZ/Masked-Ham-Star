using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] private bool DEBUG;
    [SerializeField] private Mask currentMask;

    private InputActions input;

    private void Awake()
    {
        input = new InputActions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.ChangeMask.performed += SelectMask;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.ChangeMask.performed -= SelectMask;
    }

    // Parse the target mask index from input
    private void SelectMask(InputAction.CallbackContext context)
    {
        switch (context.control.path)
        {
            case "/Keyboard/1":
                EquipMask(0);
                break;
            case "/Keyboard/2":
                EquipMask(1);
                break;
            case "/Keyboard/3":
                EquipMask(2);
                break;
            case "/Keyboard/4":
                EquipMask(3);
                break;
        }
    }

    private void EquipMask(int index)
    {
        if (DEBUG)
        {
            Debug.Log($"Equipping mask {index}");
        }
    }

}