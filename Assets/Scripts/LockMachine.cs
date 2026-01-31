using UnityEngine;

// The machine that need to fixed
public class LockMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor; 
    [SerializeField] private ResourceType requiredResource; 
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
        if (playerInventory.DoesContainResource(requiredResource))
        {
            Debug.Log("Player contains required resource"); 
            playerInventory.RemoveResource(requiredResource); 
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
}
