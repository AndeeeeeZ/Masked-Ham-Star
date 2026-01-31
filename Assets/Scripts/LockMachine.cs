using UnityEngine;

// The machine that need to fixed
public class LockMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor; 
    [SerializeField] private ResourceType requiredResource; 
    [SerializeField] private ResourceInventory playerInventory; 
    [SerializeField] private SpriteRenderer resourceIconSR; 
    [SerializeField] private GameObject resourceIconBubble; 
    private SpriteRenderer sr; 
    private Color originalColor;
    private LockMachineStatus currentState; 

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        originalColor = sr.color; 
        UpdateUIDisplay(); 
        currentState = LockMachineStatus.LOOKING_FOR_RESOURCE; 
    }

    public void OnInteract()
    {
        Debug.Log("Interacted with lock machine"); 
        if (playerInventory.DoesContainResource(requiredResource))
        {
            Debug.Log("Player contains required resource"); 
            playerInventory.RemoveResource(requiredResource); 
            currentState = LockMachineStatus.FIXED; 
            UpdateUIDisplay(); 
        } 
        else
        {
            Debug.Log("Player does not contain the required resource"); 
        }
    }

    public void OnEnterRange()
    {
        sr.color = highlightColor;    
    }

    public void OnExitRange()
    {
        sr.color = originalColor; 
    }

    private void UpdateUIDisplay()
    {
        switch (currentState)
        {
            case LockMachineStatus.FIXED:
                resourceIconBubble.SetActive(false); 
                break;
            case LockMachineStatus.LOOKING_FOR_RESOURCE:
                resourceIconBubble.SetActive(true); 
                break; 
        }
    }
}

public enum LockMachineStatus
{
    LOOKING_FOR_RESOURCE, 
    FIXED
}