using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float edgePadding = 0.5f;

    private GameObject arrow;

    void Start()
    {
        if (!cam) cam = Camera.main;
        arrow = Instantiate(arrowPrefab);
        arrow.SetActive(false);
    }

    void Update()
    {
        GameObject target = GetClosestOffscreenTarget();

        if (!target)
        {
            arrow.SetActive(false);
            return;
        }

        arrow.SetActive(true);

        Vector3 viewportPos = cam.WorldToViewportPoint(target.transform.position);

        // Direction from screen center
        Vector2 dir = new Vector2(
            viewportPos.x - 0.5f,
            viewportPos.y - 0.5f
        );

        // Find intersection with screen edge
        float scale = 0.5f / Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
        dir *= scale;

        Vector2 edgeViewportPos = new Vector2(
            0.5f + dir.x,
            0.5f + dir.y
        );

        // Apply padding
        edgeViewportPos.x = Mathf.Clamp(edgeViewportPos.x, edgePadding, 1 - edgePadding);
        edgeViewportPos.y = Mathf.Clamp(edgeViewportPos.y, edgePadding, 1 - edgePadding);

        Vector3 worldPos = cam.ViewportToWorldPoint(
            new Vector3(edgeViewportPos.x, edgeViewportPos.y, cam.nearClipPlane)
        );

        arrow.transform.position = worldPos;

        // Rotate arrow (sprite must face UP by default)
        Vector2 worldDir = (target.transform.position - arrow.transform.position).normalized;
        float angle = Mathf.Atan2(worldDir.y, worldDir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    GameObject GetClosestOffscreenTarget()
    {
        GameObject closest = null;
        float minDist = float.MaxValue;

        foreach (GameObject t in targets)
        {
            if (!t) continue;

            Vector3 vp = cam.WorldToViewportPoint(t.transform.position);

            bool onScreen =
                vp.x > 0 && vp.x < 1 &&
                vp.y > 0 && vp.y < 1 &&
                vp.z > 0;

            if (onScreen) continue;

            float dist = Vector2.Distance(cam.transform.position, t.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = t;
            }
        }

        return closest;
    }
}
