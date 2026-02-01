using UnityEngine;
using UnityEngine.U2D.Animation;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] private ResourceInventory playerInventory;
    [SerializeField] private SpriteRenderer resource1SR, resource2SR; 
    [SerializeField] private SpriteLibraryAsset spriteLibrary;

    private void Start()
    {
        UpdateInventoryDisplay(); 
    }

    public void UpdateInventoryDisplay()
    {
        SetSprite(resource1SR, playerInventory.resource1); 
        SetSprite(resource2SR, playerInventory.resource2); 
    }

    private void SetSprite(SpriteRenderer sr, ResourceType resource)
    {
        if (resource == ResourceType.NONE)
        {
            sr.sprite = null; 
            return;
        }
        sr.sprite = spriteLibrary.GetSprite("ResourceUI", resource.ToString()); 
    }
    
}
