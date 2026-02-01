using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform targetLocation; 
    [SerializeField] private Color highlightColor; 
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer spriteRenderer; 
    private Color originalColor; 

    private void Start()
    {
        originalColor = spriteRenderer.color; 
    }
    public void OnInteract()
    {
        player.transform.position = targetLocation.position; 
    }

    public void OnEnterRange()
    {
        spriteRenderer.color = highlightColor;
    }

    public void OnExitRange()
    {
        spriteRenderer.color = originalColor;
    }
}
