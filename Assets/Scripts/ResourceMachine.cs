using UnityEngine;

public class ResourceMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor;
    [SerializeField] private ResourceType produceResource;
    [SerializeField] private ResourceInventory playerInventory;
    [SerializeField] private ResourceIcon resourceIcon;
    [SerializeField] private ProgressBar progressBar; 
    [SerializeField] private float maxTime;
    private SpriteRenderer sr;
    private Color originalColor;
    private float currentTime;
    private bool isFilled;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        Reset();
    }

    private void Update()
    {
        if (!isFilled)
        {
            currentTime += Time.deltaTime;
            progressBar.UpdateProgressBar(currentTime / maxTime); 
            if (currentTime >= maxTime)
            {
                isFilled = true;
                resourceIcon.UpdateIcon(produceResource);
            }
            
        }
    }

    public void OnInteract()
    {
        if (isFilled)
        {
            playerInventory.AddResource(produceResource);
            Reset(); 
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

    private void Reset()
    {
        currentTime = 0f;
        isFilled = false;
        resourceIcon.UpdateIcon(ResourceType.NONE);
    }
}
