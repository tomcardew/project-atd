using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUIController : MonoBehaviour
{
    // Private properties
    private VisualElement root; // Root element of the UI
    private Label moneyValue; // Label to display money value
    private Label foodValue; // Label to display food value
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
        foodValue = root.Q<Label>("FoodValue");
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
            bool moneyLimitReached = Manager.Resources.HasReachedLimit(ResourceType.Money);
            moneyValue.text = Manager.Resources.GetResourceValue(ResourceType.Money).ToString();
            moneyValue.style.color = moneyLimitReached ? Color.red : Color.white;
            moneyValue.style.unityFontStyleAndWeight = moneyLimitReached
                ? FontStyle.Bold
                : FontStyle.Normal;

            bool foodLimitReached = Manager.Resources.HasReachedLimit(ResourceType.Food);
            foodValue.text = Manager.Resources.GetResourceValue(ResourceType.Food).ToString();
            foodValue.style.color = foodLimitReached ? Color.red : Color.white;
            foodValue.style.unityFontStyleAndWeight = foodLimitReached
                ? FontStyle.Bold
                : FontStyle.Normal;

            bool woodLimitReached = Manager.Resources.HasReachedLimit(ResourceType.Wood);
            woodValue.text = Manager.Resources.GetResourceValue(ResourceType.Wood).ToString();
            woodValue.style.color = woodLimitReached ? Color.red : Color.white;
            woodValue.style.unityFontStyleAndWeight = woodLimitReached
                ? FontStyle.Bold
                : FontStyle.Normal;

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
            if (Manager.Game.isOnRestTime)
            {
                roundProgressBar.title = $"Next Wave: {(int)Manager.Game.timeBeforeNextWave}";
                roundProgressBar.value =
                    1 - Manager.Game.timeBeforeNextWave / Manager.Game.baseCountingTime;
            }
            else if (Manager.Game.isOnWave)
            {
                roundProgressBar.title = $"{(int)Manager.Game.timeBeforeNextWave}";
                roundProgressBar.value =
                    Manager.Game.timeBeforeNextWave / Manager.Game.baseCountingTime;
            }
            else
            {
                roundProgressBar.title = $"Preparation: {(int)Manager.Game.timeBeforeNextWave}";
                roundProgressBar.value =
                    1 - Manager.Game.timeBeforeNextWave / Manager.Game.baseCountingTime;
            }
        }
    }
}
