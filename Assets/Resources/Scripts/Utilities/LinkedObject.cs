using UnityEngine;

public class LinkedObject : MonoBehaviour
{
    public GameObject linkedObject;

    void OnDestroy()
    {
        if (linkedObject != null)
        {
            Destroy(linkedObject);
        }
    }
}
