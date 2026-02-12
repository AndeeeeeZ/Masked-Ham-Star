using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] private bool DEBUG;
    [SerializeField] private GameObject[] worlds;
    [SerializeField] private Color[] floorColors;
    [SerializeField] private SpriteRenderer[] toChangeColor;
    [SerializeField] private Sprite[] maskSprites;
    [SerializeField] private SpriteRenderer maskSr;
    [SerializeField] private GameState gameState;
    [SerializeField] private VoidEvent[] onEquipMaskEvents;
    [SerializeField] private VoidEvent[] onUnequipMaskEvents;


    private InputActions input;
    private int currentMaskIndex;

    private void Awake()
    {
        input = new InputActions();
    }

    private void Start()
    {
        currentMaskIndex = 0;
        EquipMask(0);
        for(int i = 1; i < onUnequipMaskEvents.Length; i++)
        {
            onUnequipMaskEvents[i].Raise(); 
        }
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
        if (index > gameState.currentState)
            return;

        // Take off current mask
        if (currentMaskIndex >= 0 &&
            currentMaskIndex < onUnequipMaskEvents.Length &&
            onUnequipMaskEvents[currentMaskIndex] != null)
        {
            onUnequipMaskEvents[currentMaskIndex].Raise();
        }
        worlds[currentMaskIndex].SetActive(false);

        currentMaskIndex = index;

        worlds[currentMaskIndex].SetActive(true);
        ChangeColor(index);
        ChangeMask(index);

        if (currentMaskIndex >= 0 &&
            currentMaskIndex < onEquipMaskEvents.Length &&
            onEquipMaskEvents[currentMaskIndex] != null)
        {
            onEquipMaskEvents[currentMaskIndex].Raise();
        }
    }

    void ChangeMask(int index)
    {
        maskSr.sprite = maskSprites[index];
    }

    private void ChangeColor(int index)
    {
        for (int i = 0; i < toChangeColor.Length; i++)
        {
            toChangeColor[i].color = floorColors[index];
        }
    }

}