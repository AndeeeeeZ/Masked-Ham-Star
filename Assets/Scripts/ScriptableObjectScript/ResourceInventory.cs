using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ResourceInventory")]
public class ResourceInventory : ScriptableObject
{
    public ResourceType resource1, resource2; 

    public void Reset()
    {
        resource1 = resource2 = ResourceType.NONE;
    }

    public bool DoesContainResource(ResourceType r)
    {
        return r == resource1 || r == resource2; 
    }

    public bool RemoveResource(ResourceType r)
    {
        if (!DoesContainResource(r))
            return false;
        if (r == resource1)
        {
            resource1 = ResourceType.NONE; 
            if (resource2 != ResourceType.NONE)
            {
                resource1 = resource2; 
                resource2 = ResourceType.NONE;
            }
        }
        else if (r == resource2)
        {
            resource2 = ResourceType.NONE; 
        }
        else
        {
            Debug.LogWarning("ERROR: Impossible case"); 
            return false; 
        }
        return true; 
    }

    public void AddResource(ResourceType r)
    {
        if (resource1 == ResourceType.NONE)
        {
            resource1 = r; 
        }
        else if (resource2 == ResourceType.NONE)
        {
            resource2 = r; 
        }
        else // Both slots are full
        {
            resource1 = resource2; 
            resource2 = r; 
        }
    }
}
