using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    // Public properties
    public Vector3 target; // Where to move
    public float initialSpeed; // Initial speed of the object
    public float speedSpreadRate = 0.05f; // Rate at which the speed slightly changes
    public float speedMultiplier = 1.0f; // Multiplier to adjust speed
    public bool shouldAvoidObstacles = true; // Flag to determine if the object should avoid obstacles
    public float targetDetectionDistance = float.MaxValue; // Distance to detect targets

    [Header("Attacker options")]
    public string internalName; // For code detection between scripts of the same type

    [NonSerialized]
    public List<string> targetIdentifiers;

    [NonSerialized]
    public List<string> targetTags;

    [NonSerialized]
    public bool useTargetIdentifiers = false;

    [NonSerialized]
    public bool useTargetTags = false;

    [NonSerialized]
    public bool move = true; // Allow movement or not

    // Abstract methods
    public abstract void Init(); // Initialize custom properties
    public abstract GameObject NextTarget(GameObject target); // Set a custom target

    // Private properties
    private float spreadedSpeed;
    private float rayDistance = 0.5f; // Distance for raycasting to detect obstacles
    private Coroutine targetUpdateCoroutine; // Coroutine for updating the target

    // Computed property to get the current speed
    private float CurrentSpeed
    {
        get { return (initialSpeed + spreadedSpeed) * speedMultiplier; }
    }

    private void Start()
    {
        // Initialize properties and start the target update coroutine
        Init();
        spreadedSpeed = UnityEngine.Random.Range(-speedSpreadRate, speedSpreadRate);
        targetUpdateCoroutine = StartCoroutine(CheckForTargetUpdates());
    }

    private void FixedUpdate()
    {
        // Handle movement logic in fixed intervals
        if (move)
        {
            var direction = (target - transform.position).normalized; // Calculate direction to target
            var layer = LayerMask.GetMask("Collidables"); // Get the collision layer mask
            if (ShouldAvoidObstacles() && WillCollide(direction, layer)) // Validate the object won't collide
            {
                direction = FindBestDirection(direction, layer); // Update direction to avoid collision
            }
            transform.position += direction * CurrentSpeed * Time.fixedDeltaTime; // Move the object
        }
    }

    private void OnDisable()
    {
        // Stop the target update coroutine if it is running
        if (targetUpdateCoroutine != null)
        {
            StopCoroutine(targetUpdateCoroutine);
            targetUpdateCoroutine = null;
        }
    }

    /// <summary>
    /// Checks if moving in the given direction will result in a collision.
    /// </summary>
    /// <param name="direction">The direction to check for potential collisions.</param>
    /// <param name="layer">The collision layer to consider.</param>
    /// <returns>True if a collision will occur, otherwise false.</returns>
    private bool WillCollide(Vector3 direction, LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, layer);
        return hit.collider != null;
    }

    private bool ShouldAvoidObstacles()
    {
        // Determine if the object should avoid obstacles based on the distance to the target
        return shouldAvoidObstacles;
    }

    /// <summary>
    /// Finds the best direction to move in, avoiding collisions.
    /// </summary>
    /// <param name="direction">The initial direction to evaluate.</param>
    /// <param name="layer">The collision layer to consider.</param>
    /// <returns>The best direction to move in that does not collide with objects in the specified layer.</returns>
    private Vector2 FindBestDirection(Vector2 direction, LayerMask layer)
    {
        Vector3 rotatedDirection = direction;
        for (float i = 30f; i <= 360f; i += 30f)
        {
            rotatedDirection = Utils.RotateVector2(direction, i).normalized;
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                rotatedDirection,
                rayDistance,
                layer
            );
            if (hit.collider == null)
            {
                break;
            }
        }
        return rotatedDirection;
    }

    /// <summary>
    /// Coroutine to periodically check for target updates.
    /// </summary>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator CheckForTargetUpdates()
    {
        while (true)
        {
            GameObject nextTarget = NextTarget(GetTarget()); // Get the next target
            if (nextTarget != null)
            {
                move = true;
                target = nextTarget.transform.position; // Update the target position
            }
            else
            {
                move = false;
            }
            yield return new WaitForSeconds(0.1f); // Wait for 1 second before the next update
        }
    }

    private GameObject GetTarget()
    {
        List<GameObject> targets = new();
        if (useTargetIdentifiers)
        {
            targets.Add(FindNextTargetWithIdentifiers());
        }
        if (useTargetTags)
        {
            targets.Add(FindNextTargetWithTags());
        }
        var _t = targets
            .NotNull()
            .OrderBy(t => Vector2.Distance(t.transform.position, transform.position))
            .FirstOrDefault();
        return _t;
    }

    private GameObject FindNextTargetWithIdentifiers()
    {
        Movable[] mvs = Utils.FindAllNearObjectsOfType<Movable>(
            transform.position,
            targetDetectionDistance
        );
        Movable movable = mvs.Where(mv => targetIdentifiers.Contains(mv.internalName))
            .OrderBy(mv => Vector2.Distance(mv.transform.position, transform.position))
            .FirstOrDefault();
        if (movable != null)
        {
            return movable.gameObject;
        }
        return null;
    }

    private GameObject FindNextTargetWithTags()
    {
        return Utils.FindTheNearestGameObjectWithTags(
            transform,
            targetTags.ToArray(),
            targetDetectionDistance
        );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetDetectionDistance);
    }
}
