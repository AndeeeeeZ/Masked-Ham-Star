using UnityEngine;
using UnityEngine.U2D.Animation;

public class ResourceIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpriteLibraryAsset spriteLibrary;

    public void UpdateIcon(ResourceType r)
    {
        if (r == ResourceType.NONE)
        {
            sr.sprite = null;
            return;
        }
        sr.sprite = spriteLibrary.GetSprite("ResourceUI", r.ToString());
    }
}
