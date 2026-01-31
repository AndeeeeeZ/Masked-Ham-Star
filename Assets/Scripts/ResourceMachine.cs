using UnityEngine;

public class ResourceMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor;
    [SerializeField] private ResourceType produceResource;
    [SerializeField] private ResourceInventory playerInventory;
    private SpriteRenderer sr;
    private Color originalColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void OnInteract()
    {
        Debug.Log("Interacted with lock machine");
        playerInventory.AddResource(produceResource); 
    }

    public void OnEnterRange()
    {
        sr.color = highlightColor;
    }

    public void OnExitRange()
    {
        sr.color = originalColor;
    }
}
