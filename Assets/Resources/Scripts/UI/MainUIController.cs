using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUIController : MonoBehaviour
{
    // Private properties
    private VisualElement root; // Root element of the UI
    private Label moneyValue; // Label to display money value
    private Label woodValue; // Label to display wood value
    private Label faithValue; // Label to display faith value
    private Label populationValue; // Label to display population value
    private Label tooltipValue; // Label to display tooltip value
    private ProgressBar roundProgressBar; // Progress bar to display money value

    private void Start()
    {
        // Initialize UI elements
        root = GetComponent<UIDocument>().rootVisualElement;
        moneyValue = root.Q<Label>("MoneyValue");
        woodValue = root.Q<Label>("WoodValue");
        faithValue = root.Q<Label>("FaithValue");
        populationValue = root.Q<Label>("PeopleValue");
        tooltipValue = root.Q<Label>("Tooltip");
        roundProgressBar = root.Q<ProgressBar>("RoundProgressBar");
    }

    private void Update()
    {
        UpdateValues();
        UpdateRoundProgressBar();
    }

    private void UpdateValues()
    {
        if (Manager.Resources != null)
        {
            moneyValue.text = Manager.Resources.GetResourceValue(ResourceType.Money).ToString();
            woodValue.text = Manager.Resources.GetResourceValue(ResourceType.Wood).ToString();
            faithValue.text = Manager.Resources.GetResourceValue(ResourceType.Faith).ToString();
            populationValue.text = Manager.Population.GetPopulation().ToString();
            tooltipValue.text = Manager.UI.GetTooltip();
        }
    }

    private void UpdateRoundProgressBar()
    {
        // Update the round progress bar value
        if (Manager.Game != null)
        {
            roundProgressBar.title = $"Next Wave: {(int)Manager.Game.timeBeforeNextWave}";
            roundProgressBar.value =
                1 - Manager.Game.timeBeforeNextWave / Manager.Game.baseCountingTime;
        }
    }
}
