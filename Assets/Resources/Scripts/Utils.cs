using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils
{
    // Rotates a Vector2 by a specified angle using a Quaternion rotation.
    public static Vector2 RotateVector2(Vector2 vector, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return rotation * vector;
    }

    // Returns a random position on the perimeter of a circle.
    public static Vector2 GetRandomPositionOnCirclePerimeter(Vector2 center, float radius)
    {
        // Generate a random angle in radians
        float randomAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);

        // Calculate the x and y position based on the angle and radius
        float x = center.x + radius * Mathf.Cos(randomAngle);
        float y = center.y + radius * Mathf.Sin(randomAngle);

        // Return the new position as a Vector2
        return new Vector2(x, y);
    }

    // Finds the nearest GameObject with a specific tag within a certain maximum distance from a given origin Transform.
    public static GameObject FindTheNearestGameObjectWithTag(
        Transform origin,
        string tag,
        float maxDistance
    )
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearestObj = null;
        float maxDistanceSquared = maxDistance * maxDistance;
        float shortestDistanceSquared = maxDistanceSquared;

        foreach (var obj in objects)
        {
            float distanceSquared = (origin.position - obj.transform.position).sqrMagnitude;
            if (distanceSquared < shortestDistanceSquared)
            {
                nearestObj = obj;
                shortestDistanceSquared = distanceSquared;
            }
        }

        return nearestObj;
    }

    // Finds the nearest Movable GameObject with a specific internal name within a certain maximum distance from a given origin Transform.
    public static GameObject FindMovableGameObjectsWithInternalName(Transform origin, string name)
    {
        Movable[] movables = GameObject.FindObjectsByType<Movable>(FindObjectsSortMode.None);
        Movable found = movables
            .Where(m => m.internalName == name)
            .OrderBy(m => Vector2.Distance(m.transform.position, origin.position))
            .FirstOrDefault();
        if (found != null)
        {
            return found.gameObject.transform.parent.gameObject;
        }
        return null;
    }

    // Finds all game objects with a specified tag within a certain maximum distance from a given origin point.
    public static GameObject[] FindAllNearGameObjectsWithTag(
        Transform origin,
        string tag,
        float maxDistance
    )
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        float maxDistanceSquared = maxDistance * maxDistance;
        List<GameObject> nearObjects = new List<GameObject>(objects.Length);

        foreach (var obj in objects)
        {
            float distanceSquared = (origin.position - obj.transform.position).sqrMagnitude;
            if (distanceSquared <= maxDistanceSquared)
            {
                nearObjects.Add(obj);
            }
        }

        return nearObjects.ToArray();
    }

    // Generates a trigger collider with a specified radius at a given position within a specified transform.
    public static GameObject GenerateTriggerCollider(
        Transform transform,
        Vector3 position,
        float radius
    )
    {
        GameObject triggerColliderObject = new GameObject("WorkerCollisionArea");
        triggerColliderObject.transform.SetParent(transform);
        triggerColliderObject.transform.localPosition = position;
        triggerColliderObject.layer = LayerMask.NameToLayer("Workers");
        CircleCollider2D collider = triggerColliderObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = radius;
        return triggerColliderObject;
    }

    // Returns a random position within the camera's visible area.
    public static Vector3 GetRandomPositionInsideCamera(
        Camera camera,
        float bottomOffset = 1f,
        float uiHeight = 100f
    )
    {
        if (camera == null)
        {
            Debug.LogError("Camera is null. Make sure to pass a valid camera reference.");
            return Vector3.zero;
        }

        // Get the camera's orthographic bounds
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        // Calculate the bounds of the visible area
        Vector3 cameraPosition = camera.transform.position;
        float minX = cameraPosition.x - cameraWidth / 2;
        float maxX = cameraPosition.x + cameraWidth / 2;
        float minY = cameraPosition.y - cameraHeight / 2;
        float maxY = cameraPosition.y + cameraHeight / 2;

        // Adjust minY to account for the UI height
        minY += uiHeight / Screen.height * cameraHeight;

        // Generate a random position within the bounds, excluding the UI area
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY + bottomOffset, maxY);

        return new Vector3(randomX, randomY, 0f);
    }

    // Returns a random position outside the camera's visible area.
    public static Vector2 GetRandomPositionOutsideCamera(Camera camera)
    {
        Vector2 screenBounds = camera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, camera.transform.position.z)
        );
        float x = UnityEngine.Random.Range(-screenBounds.x * 2, screenBounds.x * 2);
        float y = UnityEngine.Random.Range(-screenBounds.y * 2, screenBounds.y * 2);

        if (x > -screenBounds.x && x < screenBounds.x)
        {
            x = x < 0 ? -screenBounds.x - 1 : screenBounds.x + 1;
        }
        if (y > -screenBounds.y && y < screenBounds.y)
        {
            y = y < 0 ? -screenBounds.y - 1 : screenBounds.y + 1;
        }

        return new Vector2(x, y);
    }

    // Converts a Unity Sprite object into a Texture2D object.
    public static Texture2D SpriteToTexture2D(Sprite sprite)
    {
        // Create a new Texture2D with the same dimensions as the sprite
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

        // Get the sprite's pixels
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height
        );

        // Set the pixels to the new texture
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }

    // Resizes a given Texture2D to a new width and height using bilinear filtering.
    public static Texture2D ResizeTexture(Texture2D sourceTexture, int newWidth, int newHeight)
    {
        // Create a new, empty Texture2D with the desired dimensions
        Texture2D resizedTexture = new Texture2D(newWidth, newHeight, sourceTexture.format, false);

        // Resize using bilinear filtering
        Color[] newPixels = new Color[newWidth * newHeight];
        float xRatio = (float)sourceTexture.width / newWidth;
        float yRatio = (float)sourceTexture.height / newHeight;

        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                // Get the nearest pixel from the original texture
                int sourceX = Mathf.FloorToInt(x * xRatio);
                int sourceY = Mathf.FloorToInt(y * yRatio);

                newPixels[y * newWidth + x] = sourceTexture.GetPixel(sourceX, sourceY);
            }
        }

        // Set the pixels on the resized texture
        resizedTexture.SetPixels(newPixels);
        resizedTexture.Apply();

        return resizedTexture;
    }

    // Calculates a rate based on the input value, with interpolation between max and min values within specified ranges.
    public static float CalculateRate(
        float value,
        float max,
        float min,
        float baseValue,
        float maxValue
    )
    {
        // Ensure the input values are valid
        if (baseValue >= maxValue)
            throw new ArgumentException("baseValue should be less than maxValue.");
        if (min > max)
            throw new ArgumentException("min should be less than or equal to max.");

        // If value is less than or equal to baseValue, return max
        if (value <= baseValue)
        {
            return max;
        }

        // If value is greater than or equal to maxValue, return min
        if (value >= maxValue)
        {
            return min;
        }

        // Interpolate the rate between max and min
        float t = (value - baseValue) / (maxValue - baseValue);
        return max - t * (max - min);
    }

    // Finds the nearest GameObject with any of the specified tags within a certain maximum distance from a given origin Transform.
    public static GameObject FindTheNearestGameObjectWithTags(
        Transform origin,
        string[] tags,
        float maxDistance
    )
    {
        GameObject nearestObj = null;
        float maxDistanceSquared = maxDistance * maxDistance;
        float shortestDistanceSquared = maxDistanceSquared;

        foreach (var tag in tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (var obj in objects)
            {
                float distanceSquared = (origin.position - obj.transform.position).sqrMagnitude;
                if (distanceSquared < shortestDistanceSquared)
                {
                    nearestObj = obj;
                    shortestDistanceSquared = distanceSquared;
                }
            }
        }

        return nearestObj;
    }

    // Finds the nearest GameObject with a target armor level within a certain maximum distance from a given origin Transform.
    public static GameObject FindTheNearestObjectWithTargetArmorLevel(
        Transform origin,
        ArmorLevel targetArmorLevel,
        float maxDistance = float.MaxValue,
        string filterByTag = null
    )
    {
        GameObject nearestObj = null;
        float maxDistanceSquared = maxDistance * maxDistance;
        float shortestDistanceSquared = maxDistanceSquared;

        Damageable[] objects = GameObject
            .FindObjectsByType<Damageable>(FindObjectsSortMode.None)
            .Select(o => o.GetComponentInChildren<Damageable>())
            .Where(d => d.armorLevel <= targetArmorLevel)
            .ToArray();

        if (filterByTag != null)
        {
            objects = objects
                .Where(o => o.transform.parent.gameObject.CompareTag(filterByTag))
                .ToArray();
        }
        foreach (var obj in objects)
        {
            float distanceSquared = (origin.position - obj.transform.position).sqrMagnitude;
            if (distanceSquared < shortestDistanceSquared)
            {
                nearestObj = obj.transform.parent.gameObject;
                shortestDistanceSquared = distanceSquared;
            }
        }

        return nearestObj;
    }

    // Finds all objects of a specified type within a certain maximum distance from a given origin point.
    public static T[] FindAllNearObjectsOfType<T>(Vector3 origin, float maxDistance)
        where T : Component
    {
        T[] objects = GameObject.FindObjectsByType<T>(FindObjectsSortMode.None);
        float maxDistanceSquared = maxDistance * maxDistance;
        List<T> nearObjects = new();

        foreach (var obj in objects)
        {
            float distanceSquared = (origin - obj.transform.position).sqrMagnitude;
            if (distanceSquared <= maxDistanceSquared)
            {
                nearObjects.Add(obj);
            }
        }

        return nearObjects.ToArray();
    }

    // Returns a random position on the Camera visible area perimeter with an offset towards the center.
    public static Vector3 GetPositionOnBorder(Camera camera, float offset, float uiHeight = 100f)
    {
        if (camera == null)
        {
            Debug.LogError("Camera is null. Make sure to pass a valid camera reference.");
            return Vector3.zero;
        }

        // Get the camera's orthographic bounds
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        // Calculate the bounds of the visible area
        Vector3 cameraPosition = camera.transform.position;
        float minX = cameraPosition.x - cameraWidth / 2;
        float maxX = cameraPosition.x + cameraWidth / 2;
        float minY = cameraPosition.y - cameraHeight / 2;
        float maxY = cameraPosition.y + cameraHeight / 2;

        // Adjust minY to account for the UI height
        minY += uiHeight / Screen.height * cameraHeight;

        // Determine which side of the perimeter to place the position
        int side = UnityEngine.Random.Range(0, 1);
        float x = 0,
            y = 0;

        switch (side)
        {
            case 0: // Top
                x = UnityEngine.Random.Range(minX, maxX);
                y = maxY;
                break;
            // case 1: // Bottom
            //     x = UnityEngine.Random.Range(minX, maxX);
            //     y = minY;
            //     break;
            // case 2: // Left
            case 1:
                x = minX;
                y = UnityEngine.Random.Range(minY, maxY);
                break;
            // case 3: // Right
            //     x = maxX;
            //     y = UnityEngine.Random.Range(minY, maxY);
            //     break;
        }

        // Apply offset towards the center
        Vector3 position = new Vector3(x, y, 0f);
        Vector3 directionToCenter = (cameraPosition - position).normalized;
        position += directionToCenter * offset;

        return position;
    }

    // Returns the opposite corner of a given position, optionally outside the camera view.
    public static Vector3 GetOppositeCorner(Vector3 position, bool outsideView, float offset = 2.0f)
    {
        Camera camera = Camera.main;
        Vector3 screenPoint = camera.WorldToViewportPoint(position);
        screenPoint.x = 1.0f - screenPoint.x;
        screenPoint.y = 1.0f - screenPoint.y;
        Vector3 oppositeCorner = camera.ViewportToWorldPoint(screenPoint);

        if (
            outsideView
            && screenPoint.x >= 0
            && screenPoint.x <= 1
            && screenPoint.y >= 0
            && screenPoint.y <= 1
        )
        {
            oppositeCorner += (oppositeCorner - position).normalized * offset;
        }

        return oppositeCorner;
    }

    // Destroys all children of the specified GameObject.
    public static void DestroyAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Shuffles a list of any type using the Fisher-Yates algorithm.
    public static List<T> ShuffleList<T>(List<T> list)
    {
        List<T> _list = new List<T>(list);
        int n = _list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = _list[k];
            _list[k] = _list[n];
            _list[n] = value;
        }
        return _list;
    }

    // Rotates towards the target direction.
    public static void RotateTowardsTarget(Vector3 target, Transform transform, float offset = 0f)
    {
        if (target != null)
        {
            Vector3 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        }
    }

    // Finds an available ResourceGenerator within a specified range that has capacity for more workers.
    public static ResourceGenerator GetAvailableResource(Vector3 position, float range = 10f)
    {
        ResourceGenerator[] generators = FindAllNearObjectsOfType<ResourceGenerator>(
            position,
            range
        );

        // Order generators by distance to the position
        var sortedGenerators = generators
            .Where(generator => generator != null && generator.HasCapacity)
            .OrderBy(generator => Vector2.Distance(generator.transform.position, position));

        // Return the nearest generator with capacity
        return sortedGenerators.FirstOrDefault();
    }

    // Checks if a position is on a line defined by two points.
    public static bool IsPositionOnLine(Vector3 position, Vector3 lineStart, Vector3 lineEnd)
    {
        float distance = Vector3.Distance(lineStart, lineEnd);
        float distanceToStart = Vector3.Distance(position, lineStart);
        float distanceToEnd = Vector3.Distance(position, lineEnd);

        // Check if the position is close to the line within a small threshold
        return Mathf.Abs(distance - (distanceToStart + distanceToEnd)) < 1f;
    }

    // Checks if a position is too close to any position in a list of positions.
    public static bool IsPositionTooClose(
        Vector3 position,
        List<Vector3> positions,
        float minDistance
    )
    {
        foreach (Vector3 pos in positions)
        {
            if (Vector3.Distance(position, pos) < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    // Gets the color associated with a specific CardType.
    public static Color GetForegroundTypeColor(CardType type)
    {
        switch (type)
        {
            case CardType.Normal:
                return new Color(0.84f, 0.84f, 0.84f);
            case CardType.Action:
                return new Color(0.97f, 1f, 0.72f);
            case CardType.Structure:
                return new Color(0.72f, 0.74f, 1f);
            case CardType.WoodResource:
                return new Color(0.72f, 1f, 0.79f);
            default:
                return Color.black;
        }
    }

    // Gets the name associated with a specific CardType.
    public static string GetForegroundTypeName(CardType type)
    {
        switch (type)
        {
            case CardType.Normal:
                return "Hand Action";
            case CardType.Action:
                return "Action";
            case CardType.Structure:
                return "Structure";
            case CardType.WoodResource:
                return "Resource";
            default:
                return "Unknown";
        }
    }

    // Gets the total value of multipliers for a specific key.
    public static float GetMultipliersValue(List<MultiplierItem> multipliers, string key)
    {
        return multipliers.Where(m => m.key == key).Sum(m => m.value);
    }
}
