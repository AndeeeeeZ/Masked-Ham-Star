using UnityEngine;

public class Cat : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor; 
    private Color originalColor; 
    private SpriteRenderer sr;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); 
    }

    private void Start()
    {
        originalColor = sr.color; 
    }
    public void OnEnterRange()
    {
        sr.color = highlightColor; 
    }

    public void OnExitRange()
    {
        sr.color = originalColor; 
    }

    public void OnInteract()
    {
        gameObject.SetActive(false); 
    }

    public void ShowCat()
    {
        sr.enabled = true; 
    }
    public void HideCat()
    {
        sr.enabled = false; 
    }

    public void EnableCat()
    {
        gameObject.SetActive(true)  ; 
    }
}
