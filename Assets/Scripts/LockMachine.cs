using UnityEngine;

// The machine that need to fixed
public class LockMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor;
    [SerializeField] private ResourceType requiredResource;
    [SerializeField] private ResourceInventory playerInventory;
    [SerializeField] private ResourceIcon resourceIcon;
    [SerializeField] private GameObject resourceIconBubble;
    [SerializeField] private int amountRequiredToFix;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private GameState gameState;
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
        GetNextRequiredResource(); 
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
            GetNextRequiredResource(); 
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

    private void GetNextRequiredResource()
    {
        int index = Random.Range(0, gameState.currentState + 1);
        switch (index)
        {
            case 0:
                requiredResource = ResourceType.WRENCH;
                break;
            case 1:
                requiredResource = ResourceType.CANDLE;
                break;
            case 2:
                requiredResource = ResourceType.COG;
                break;
            case 3:
                requiredResource = ResourceType.CANDY;
                break;
        }
        UpdateRequiredResource(requiredResource); 
    }

    private void UpdateRequiredResource(ResourceType r)
    {
        resourceIcon.UpdateIcon(r);
    }
}

public enum LockMachineStatus
{
    LOOKING_FOR_RESOURCE,
    FIXED
}