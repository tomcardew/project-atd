using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardBackUIController : MonoBehaviour
{
    // Public properties
    public int count; // The count of cards in the pile
    public string pileName; // The name of the pile

    // Private properties
    private TextMeshProUGUI countLabel; // UI label to display the count
    private TextMeshProUGUI pileNameLabel; // UI label to display the pile name

    private void Start()
    {
        // Set the world camera for the canvas to the main camera
        GetComponentInParent<Canvas>().worldCamera = Camera.main;

        // Get all TextMeshProUGUI components in children and assign them to labels
        var labels = GetComponentsInChildren<TextMeshProUGUI>();
        countLabel = labels[0];
        pileNameLabel = labels[1];

        // Initialize the UI with the current data
        SetData();
    }

    // Method to update the data and refresh the UI
    public void UpdateData(int count, string pileName)
    {
        this.count = count; // Update the count
        this.pileName = pileName; // Update the pile name
        SetData(); // Refresh the UI with the new data
    }

    // Method to set the UI elements with the current data
    private void SetData()
    {
        countLabel.text = count.ToString(); // Set the count label text
        pileNameLabel.text = pileName; // Set the pile name label text
    }
}
