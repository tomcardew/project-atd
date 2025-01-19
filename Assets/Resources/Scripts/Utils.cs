using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils
{
    /// <summary>
    /// The RotateVector2 function rotates a Vector2 by a specified angle using a Quaternion rotation.
    /// </summary>
    /// <param name="Vector2">A Vector2 represents a 2D vector with x and y components. It is commonly
    /// used in game development and graphics programming to represent positions, directions,
    /// velocities, and more in a 2D space.</param>
    /// <param name="angle">The `angle` parameter represents the amount by which you want to rotate the
    /// input vector. It is specified in degrees.</param>
    /// <returns>
    /// The method `RotateVector2` returns a new `Vector2` that is the result of rotating the input
    /// `vector` by the specified `angle` using a `Quaternion` rotation.
    /// </returns>
    public static Vector2 RotateVector2(Vector2 vector, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return rotation * vector;
    }

    /// <summary>
    /// Returns a random position on the perimeter of a circle.
    /// </summary>
    /// <param name="center">The center of the circle as a Vector2.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <returns>A random Vector2 position on the circle's perimeter.</returns>
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

    /// <summary>
    /// The function finds the nearest GameObject with a specific tag within a certain maximum distance
    /// from a given origin Transform.
    /// </summary>
    /// <param name="Transform">The `Transform` parameter in the `FindTheNearestGameObjectsWithTag`
    /// method represents the starting point or origin from which you want to find the nearest game
    /// object with a specific tag within a certain maximum distance. The `origin` parameter is a
    /// reference to a `Transform` component that holds the</param>
    /// <param name="tag">The `tag` parameter in the `FindTheNearestGameObjectsWithTag` method is a
    /// string that represents the tag of the GameObjects you want to search for. When calling this
    /// method, you would provide a specific tag value to filter and find the nearest GameObject with
    /// that tag from the specified origin</param>
    /// <param name="maxDistance">The `maxDistance` parameter in the `FindTheNearestGameObjectsWithTag`
    /// method represents the maximum distance from the `origin` within which you want to find the
    /// nearest game object with the specified tag. This distance is used to filter out game objects
    /// that are beyond this range from the `origin</param>
    /// <returns>
    /// the GameObject that is nearest to the specified origin Transform and has the specified tag
    /// within the maximum distance specified.
    /// </returns>
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

    /// <summary>
    /// The function finds the nearest Movable GameObject with a specific internal name within a certain maximum distance
    /// from a given origin Transform.
    /// </summary>
    /// <param name="origin">The starting point or origin from which you want to find the nearest Movable GameObject.</param>
    /// <param name="name">The internal name of the Movable GameObject you want to find.</param>
    /// <param name="distance">The maximum distance from the origin within which you want to find the Movable GameObject.</param>
    /// <returns>The nearest Movable GameObject with the specified internal name.</returns>
    public static GameObject FindMovableGameObjectsWithInternalName(
        Transform origin,
        string name,
        float distance = float.MaxValue
    )
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

    /// <summary>
    /// The function `FindAllNearGameObjectsWithTag` finds all game objects with a specified tag within
    /// a certain maximum distance from a given origin point.
    /// </summary>
    /// <param name="Transform">A Transform represents the position, rotation, and scale of an object in
    /// Unity. It is commonly used to manipulate the position and orientation of GameObjects in a
    /// scene.</param>
    /// <param name="tag">The `tag` parameter in the `FindAllNearGameObjectsWithTag` method is a string
    /// that represents the tag of the GameObjects you want to find. GameObjects in Unity can be
    /// assigned tags to categorize them for easier identification and manipulation in scripts.</param>
    /// <param name="maxDistance">The `maxDistance` parameter in the `FindAllNearGameObjectsWithTag`
    /// method represents the maximum distance from the `origin` within which you want to find game
    /// objects with the specified tag. This distance is used to filter out game objects that are
    /// farther away than the specified maximum distance.</param>
    /// <returns>
    /// An array of GameObjects that are within a specified maximum distance from a given origin
    /// Transform and have a specified tag.
    /// </returns>
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

    /// <summary>
    /// The function generates a trigger collider with a specified radius at a given position within a
    /// specified transform.
    /// </summary>
    /// <param name="Transform">A Transform component that represents the parent object where the
    /// trigger collider will be created.</param>
    /// <param name="Vector3">A Vector3 is a data structure in Unity that represents a point or
    /// direction in 3D space. It consists of three float values: x, y, and z. These values can be used
    /// to store positions, rotations, scales, or directions in a 3D environment.</param>
    /// <param name="radius">The `radius` parameter in the `GenerateTriggerCollider` method represents
    /// the radius of the CircleCollider2D that will be added to the GameObject. This radius determines
    /// the size of the circular trigger collider that will be created around the specified
    /// position.</param>
    /// <returns>
    /// The method `GenerateTriggerCollider` returns a GameObject that represents a trigger collider
    /// with a circle shape.
    /// </returns>
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

    /// <summary>
    /// Returns a random position within the camera's visible area.
    /// </summary>
    /// <param name="camera">The camera to calculate the visible area.</param>
    /// <returns>A random position within the visible area.</returns>
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

    /// <summary>
    /// The function `SpriteToTexture2D` converts a Unity `Sprite` object into a `Texture2D` object.
    /// </summary>
    /// <param name="Sprite">The `SpriteToTexture2D` method you provided converts a Sprite object into a
    /// Texture2D object. The parameters used in the method are:</param>
    /// <returns>
    /// The method `SpriteToTexture2D` returns a `Texture2D` object that represents the sprite converted
    /// into a texture.
    /// </returns>
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

    /// <summary>
    /// The function ResizeTexture resizes a given Texture2D to a new width and height using bilinear
    /// filtering.
    /// </summary>
    /// <param name="Texture2D">The `Texture2D` class in Unity represents a 2D texture that can be used
    /// for rendering on objects in the scene. It contains pixel data that defines the color and
    /// transparency of each texel (texture element).</param>
    /// <param name="newWidth">The `newWidth` parameter in the `ResizeTexture` method represents the
    /// desired width of the new resized texture that you want to create from the original
    /// `sourceTexture`. This parameter specifies the number of pixels in the horizontal direction for
    /// the resized texture.</param>
    /// <param name="newHeight">The `newHeight` parameter in the `ResizeTexture` method represents the
    /// desired height of the new resized texture that you want to create based on the original
    /// `sourceTexture`. This parameter specifies the number of pixels in the vertical direction for the
    /// resized texture.</param>
    /// <returns>
    /// The method `ResizeTexture` returns a new `Texture2D` object that has been resized based on the
    /// input parameters `newWidth` and `newHeight`.
    /// </returns>
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

    /// <summary>
    /// The CalculateRate function calculates a rate based on the input value, with interpolation
    /// between max and min values within specified ranges.
    /// </summary>
    /// <param name="value">The `value` parameter represents the value for which you want to calculate
    /// the rate. It is the input value for which you want to determine the rate between the `max` and
    /// `min` values based on the `baseValue` and `maxValue`.</param>
    /// <param name="max">The `max` parameter in the `CalculateRate` method represents the maximum rate
    /// value that can be returned. It is used in the calculation to determine the rate based on the
    /// input values and conditions specified in the method.</param>
    /// <param name="min">The `min` parameter in the `CalculateRate` method represents the minimum rate
    /// value that can be returned by the calculation. It is used in the interpolation process to
    /// determine the rate based on the input `value` relative to the `baseValue` and
    /// `maxValue`.</param>
    /// <param name="baseValue">The `baseValue` parameter in the `CalculateRate` method represents the
    /// starting point from which the rate calculation will be based. It is a reference value that helps
    /// determine the interpolation between the `max` and `min` values based on the input
    /// `value`.</param>
    /// <param name="maxValue">The `maxValue` parameter in the `CalculateRate` method represents the
    /// maximum possible value that `value` can take. This parameter is used in the calculation to
    /// determine the rate between the `max` and `min` values based on the relationship between `value`,
    /// `baseValue`, and `</param>
    /// <returns>
    /// The CalculateRate method returns a float value that represents the interpolated rate between the
    /// max and min values based on the input parameters provided.
    /// </returns>
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

    /// <summary>
    /// The function finds the nearest GameObject with any of the specified tags within a certain maximum distance
    /// from a given origin Transform.
    /// </summary>
    /// <param name="Transform">The `Transform` parameter in the `FindTheNearestGameObjectWithTags`
    /// method represents the starting point or origin from which you want to find the nearest game
    /// object with any of the specified tags within a certain maximum distance. The `origin` parameter is a
    /// reference to a `Transform` component that holds the</param>
    /// <param name="tags">The `tags` parameter in the `FindTheNearestGameObjectWithTags` method is an array
    /// of strings that represents the tags of the GameObjects you want to search for. When calling this
    /// method, you would provide specific tag values to filter and find the nearest GameObject with
    /// any of those tags from the specified origin</param>
    /// <param name="maxDistance">The `maxDistance` parameter in the `FindTheNearestGameObjectWithTags`
    /// method represents the maximum distance from the `origin` within which you want to find the
    /// nearest game object with any of the specified tags. This distance is used to filter out game objects
    /// that are beyond this range from the `origin</param>
    /// <returns>
    /// the GameObject that is nearest to the specified origin Transform and has any of the specified tags
    /// within the maximum distance specified.
    /// </returns>
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

    /// <summary>
    /// The function `FindAllNearObjectsOfType` finds all objects of a specified type within
    /// a certain maximum distance from a given origin point.
    /// </summary>
    /// <typeparam name="T">The type of objects to find.</typeparam>
    /// <param name="origin">The starting point or origin from which you want to find the nearest objects.</param>
    /// <param name="maxDistance">The maximum distance from the origin within which you want to find objects.</param>
    /// <returns>An array of objects of the specified type that are within the specified maximum distance from the origin.</returns>
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

    /// <summary>
    /// Returns a random position on the Camera visible area perimeter with an offset towards the center.
    /// </summary>
    /// <param name="camera">The camera to calculate the visible area.</param>
    /// <param name="offset">The offset towards the center.</param>
    /// <returns>A random position on the perimeter with an offset towards the center.</returns>
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

    public static Vector3 GetOppositeCornerOutsideView(Vector3 position, float offset = 2.0f)
    {
        Camera camera = Camera.main;
        Vector3 screenPoint = camera.WorldToViewportPoint(position);
        screenPoint.x = 1.0f - screenPoint.x;
        screenPoint.y = 1.0f - screenPoint.y;
        Vector3 oppositeCorner = camera.ViewportToWorldPoint(screenPoint);

        // Ensure the position is outside the camera view
        if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1)
        {
            oppositeCorner += (oppositeCorner - position).normalized * offset;
        }

        return oppositeCorner;
    }

    /// <summary>
    /// Destroys all children of the specified GameObject.
    /// </summary>
    /// <param name="parent">The parent GameObject whose children will be destroyed.</param>
    public static void DestroyAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Shuffles a list of any type using the Fisher-Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to shuffle.</param>
    public static List<T> ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        List<T> _list = new List<T>(list);
        int n = _list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = _list[k];
            _list[k] = _list[n];
            _list[n] = value;
        }
        return _list;
    }

    // Rotate towards the target direction
    public static void RotateTowardsTarget(Vector3 target, Transform transform, float offset = 0f)
    {
        if (target != null)
        {
            Vector3 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        }
    }

    /// <summary>
    /// Finds an available ResourceGenerator within a specified range that has capacity for more workers.
    /// </summary>
    /// <param name="position">The position from which to search for nearby ResourceGenerators.</param>
    /// <returns>
    /// A ResourceGenerator that has capacity for more workers, or null if no such generator is found.
    /// </returns>
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
}
