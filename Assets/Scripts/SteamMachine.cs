using Unity.VisualScripting;
using UnityEngine;

public class SteamMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private Color highlightColor, warningColor;
    [SerializeField] private float maxTriggerTime, maxTimeUntilExplosion;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private VoidEvent gameOver;
    private float currentTime, nextTriggerTime;
    private bool triggered, counting;
    private Color originalColor;
    private SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        counting = true;
        Reset();
    }

    private void Update()
    {
        if (!counting) return;

        currentTime += Time.deltaTime;
        UpdateProgressBar();
        if (!triggered && currentTime >= nextTriggerTime)
        {
            triggered = true;
            currentTime = 0f;
            sr.color = warningColor;
        }
        else if (triggered && currentTime >= maxTimeUntilExplosion)
        {
            // Game Over
            gameOver.Raise();
            counting = false;
        }
    }

    public void OnInteract()
    {
        if (triggered)
            Reset();
    }

    public void OnEnterRange()
    {
        if (!triggered)
            sr.color = highlightColor;
    }

    public void OnExitRange()
    {
        if (!triggered)
            sr.color = originalColor;
    }

    public void EnableSteamMachine()
    {
        gameObject.SetActive(true);
    }

    public void ShowSteamMachine()
    {
        sr.enabled = true;
        progressBar.gameObject.SetActive(true); 
    }

    public void HideSteamMachine()
    {
        sr.enabled = false;
        progressBar.gameObject.SetActive(false); 
    }

    private void Reset()
    {
        currentTime = 0f;
        triggered = false;
        GetNewTriggerTime();
        sr.color = originalColor;
    }

    private void GetNewTriggerTime()
    {
        nextTriggerTime = Random.Range(10f, maxTriggerTime);
    }

    private void UpdateProgressBar()
    {
        if (!progressBar.gameObject.activeSelf)
            return; 
        if (!triggered)
        {
            progressBar.UpdateProgressBar(0f);
        }
        else
        {
            progressBar.UpdateProgressBar(currentTime / maxTimeUntilExplosion);
        }
    }
}
