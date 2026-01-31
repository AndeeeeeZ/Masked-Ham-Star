using UnityEngine;

// The machine that need to fixed
public class LockMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor; 
    [SerializeField] private ResourceType requiredResource; 
    [SerializeField] private ResourceInventory playerInventory; 
    [SerializeField] private SpriteRenderer resourceIconSR; 
    [SerializeField] private GameObject resourceIconBubble; 
    [SerializeField] private int amountRequiredToFix; 
    [SerializeField] private ProgressBar progressBar; 
    private SpriteRenderer sr; 
    private Color originalColor;
    private LockMachineStatus currentState; 
    private int currentFixNum; 

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        originalColor = sr.color; 
        UpdateUIDisplay(); 
        currentState = LockMachineStatus.LOOKING_FOR_RESOURCE; 
        currentFixNum = 0; 
        UpdateProgressBar(); 
    }

    public void OnInteract()
    {
        if (currentState == LockMachineStatus.FIXED)
        {
            Debug.Log("Don't need to fix a good machine"); 
            return; 
        }
        if (playerInventory.DoesContainResource(requiredResource))
        {
            playerInventory.RemoveResource(requiredResource); 
            currentFixNum++; 
            if (currentFixNum >= amountRequiredToFix)
            {
                currentState = LockMachineStatus.FIXED;
            }
            UpdateProgressBar(); 
            UpdateUIDisplay();  
        } 
        else
        {
            Debug.Log("Player does not have the required resource"); 
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

    private void UpdateProgressBar()
    {
        Debug.Log((float)currentFixNum / amountRequiredToFix); 
        progressBar.UpdateProgressBar((float)currentFixNum / amountRequiredToFix); 
    }
}

public enum LockMachineStatus
{
    LOOKING_FOR_RESOURCE, 
    FIXED
}