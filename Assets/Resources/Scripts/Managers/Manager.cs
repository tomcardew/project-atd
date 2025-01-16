using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Static instance of the Manager class
    private static Manager _instance;

    // Public property to access the instance
    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Find an existing instance of the Manager in the scene
                _instance = FindFirstObjectByType<Manager>();

                // If no instance is found, create a new one
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(Manager).Name);
                    _instance = singletonObject.AddComponent<Manager>();
                }
            }
            return _instance;
        }
    }

    // Ensure the instance is not destroyed on scene load
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        // Initialize the ResourcesManager reference
        Resources = FindFirstObjectByType<ResourcesManager>();
        Game = FindFirstObjectByType<GameManager>();
        Population = FindFirstObjectByType<PopulationManager>();
        Cards = FindFirstObjectByType<CardsManager>();
        Cursor = FindFirstObjectByType<CursorManager>();
        Round = FindFirstObjectByType<RoundManager>();
        Interactions = FindFirstObjectByType<InteractionsManager>();
        UI = FindFirstObjectByType<UIManager>();
    }

    // Static reference to the ResourcesManager
    public static ResourcesManager Resources { get; private set; }

    public static GameManager Game { get; private set; }

    public static PopulationManager Population { get; private set; }

    public static CardsManager Cards { get; private set; }

    public static CursorManager Cursor { get; private set; }

    public static RoundManager Round { get; private set; }

    public static InteractionsManager Interactions { get; private set; }

    public static UIManager UI { get; private set; }

    private void Start()
    {
        Game.StartGame();
    }
}
