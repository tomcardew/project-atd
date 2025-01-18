using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float unpausableDeltaTime { get; private set; }
    public float pausableFixedDeltaTime { get; private set; }
    public float pausableDeltaTime { get; private set; }

    public bool isPaused = false;

    private void Update()
    {
        pausableDeltaTime = isPaused ? 0 : Time.deltaTime;
        unpausableDeltaTime = Time.deltaTime;
    }

    private void FixedUpdate()
    {
        pausableFixedDeltaTime = isPaused ? 0 : Time.fixedDeltaTime;
    }
}
