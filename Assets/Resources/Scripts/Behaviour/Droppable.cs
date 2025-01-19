using UnityEngine;
using System.Collections.Generic;

/**
 * Makes the gameobject position's to be the same as the cursor position until the left mouse button is pressed.
 */
public class Droppable : MonoBehaviour
{
    // Public properties
    public int index;
    public Card card; // Card to pay when the object is dropped
    public delegate void DropFinished(Card card, int index, bool success);
    public event DropFinished OnDropFinished;

    // Private properties
    private bool active; // Boolean to check if the script applies its effects
    private GameObject mainObject; // The main object to be affected by the script
    private Vector3 position; // The current position of the cursor in world space
    private Quaternion rotation; // The current rotation of the main object

    private void Start()
    {
        // Check if the prefabs are set
        if (card.droppablePrefab == null || card.prefab == null)
        {
            return;
        }
        // Activate the droppable functionality
        SetActive(true);
    }

    private void Update()
    {
        if (active)
        {
            // Update the position to the cursor's position
            position = GetCursorPosition();

            // // Snap the position to the nearest grid point
            // float gridSize = 0.5f; // Tamaño de la cuadrícula
            // position.x = Mathf.Round(position.x / gridSize) * gridSize;
            // position.y = Mathf.Round(position.y / gridSize) * gridSize;

            if (mainObject != null)
            {
                // Move the main object to the cursor's position
                mainObject.transform.position = position;
            }
            // Check if the left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // Deactivate the droppable functionality
                SetActive(false);
            }
            // Check if the 'R' key is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Rotate the main object by 90 degrees
                rotation = mainObject.transform.rotation;
                rotation *= Quaternion.Euler(0, 0, 90);
                mainObject.transform.rotation = rotation;
            }
            // Check if the 'ESC' key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Deactivate the droppable functionality
                OnDropFinished?.Invoke(card, index, false);
                Destroy(gameObject);
            }
        }
    }

    public void SetActive(bool active)
    {
        this.active = active;
        if (active)
        {
            Manager.UI.SetTooltip("Left click to set. Press 'R' to rotate. Press 'ESC' to cancel.");
            // Set the cursor to the grab cursor
            Manager.Cursor.SetGrabCursor();
            // Instantiate the droppable prefab if it's not already instantiated
            mainObject = Instantiate(
                card.droppablePrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
        }
        else
        {
            // Set the cursor to the default cursor
            Manager.Cursor.SetDefaultCursor();
            Manager.UI.ClearTooltip();
            // Instantiate the active prefab at the last known position
            Manager.Resources.Pay(card);
            OnDropFinished?.Invoke(card, index, true);
            Instantiate(card.prefab, position, rotation);
            // Destroy this script's game object
            DestroyImmediate(gameObject);
        }
    }

    private Vector3 GetCursorPosition()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        // Set the z position to the camera's near clip plane
        mousePosition.z = Camera.main.nearClipPlane;
        // Convert the screen position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return new Vector3(worldPosition.x, worldPosition.y, 0);
    }
}
