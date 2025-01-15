using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public Vector2 target;
    public float offset = -90f;

    private void Update()
    {
        Utils.RotateTowardsTarget(target, transform, offset);
    }
}
