using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private GameObject ghostSprite;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = ghostSprite.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null) return;

        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        if (direction.x != 0)
        {
            sr.flipX = direction.x < 0;
        }
    }

    public void ShowGhost()
    {
        ghostSprite.SetActive(true);
    }

    public void HideGhost()
    {
        ghostSprite.SetActive(false);
    }

    public void EnableGhost()
    {
        gameObject.SetActive(true); 
    }
}
