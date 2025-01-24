using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ResourceGenerator : MonoBehaviour
{
    // Public properties
    public ResourceType resource; // wood, faith
    public int workerCapacity; // max workers supported
    public int totalResources; // max available resources to generate
    public bool isInfinite = false; // if the generator is infinite
    public float tiringRate = 0.1f; // how much workers get tired each generation
    public float hungerRate = 0.05f; // how much workers get hungry each generation
    public float initialGenerationQuantity; // how many per worker
    public float generationQuantityMultiplier = 1.0f;
    public float initialGenerationDelay; // how often to generate
    public float generationDelayMultiplier = 1.0f;
    public GameObject emptyPrefab;
    public SliderController sliderBar; // Reference to the health bar
    public bool isPaused = false;
    private bool _oldIsPaused = false;

    // Private properties
    private Resource _resource;
    private Coroutine generationCoroutine;
    private int remainingResources;
    private float load = 0;
    private NewResourceUIController newResourceUI;
    private UIHideShow pausedIndicator;
    private UIHideShow lowCapacityIndicator;

    [SerializeField]
    private List<Person> workers;

    public bool HasCapacity
    {
        get { return workers.Count < workerCapacity; }
    }

    private bool _oldHasHungryWorkers = false;
    public bool HasHungryWorkers
    {
        get
        {
            foreach (var worker in workers)
            {
                if (worker.hunger >= 1)
                {
                    return true;
                }
            }
            return false;
        }
    }

    // Computed property to get the current generation quantity
    private float CurrentGenerationQuantity
    {
        get { return initialGenerationQuantity * generationQuantityMultiplier; }
    }

    // Computed property to get the current generation delay
    private float CurrentGenerationDelay
    {
        get { return initialGenerationDelay * generationDelayMultiplier; }
    }

    private void Start()
    {
        // Initialize the resource and start the generation coroutine
        _resource = ResourceList.GetResourceByType(resource);
        remainingResources = totalResources;
        if (sliderBar == null)
        {
            sliderBar = GetComponentInChildren<SliderController>();
        }
        newResourceUI = transform.parent.GetComponentInChildren<NewResourceUIController>();
        newResourceUI.resource = resource;
        var indicators = transform.parent.GetComponentsInChildren<UIHideShow>();
        pausedIndicator = indicators.Where(c => c.name == "Paused").First();
        lowCapacityIndicator = indicators.Where(c => c.name == "LowCapacity").First();
        generationCoroutine = StartCoroutine(GenerationCoroutine());
    }

    private void Update()
    {
        if (isPaused != _oldIsPaused)
        {
            if (isPaused)
            {
                pausedIndicator.Show();
            }
            else
            {
                pausedIndicator.Hide();
            }
            _oldIsPaused = isPaused;
        }
        if (HasHungryWorkers != _oldHasHungryWorkers)
        {
            if (HasHungryWorkers)
            {
                lowCapacityIndicator.Show();
            }
            else
            {
                lowCapacityIndicator.Hide();
            }
            _oldHasHungryWorkers = HasHungryWorkers;
        }
    }

    private void OnDisable()
    {
        if (generationCoroutine != null)
        {
            StopCoroutine(generationCoroutine);
            generationCoroutine = null;
        }
    }

    public void AddWorker(Person person)
    {
        // Add a worker to the list of workers
        this.workers.Add(person);
    }

    private IEnumerator GenerationCoroutine()
    {
        // Coroutine to generate resources over time
        while (isInfinite || remainingResources > 0)
        {
            isPaused =
                Manager.Resources.HasReachedLimit(resource)
                && (remainingResources < totalResources);
            yield return new WaitForSeconds(CurrentGenerationDelay);
            if (isPaused)
            {
                for (int i = 0; i < workers.Count; i++)
                {
                    SendWorkerHome(workers[i]);
                    workers.RemoveAt(i);
                }
                continue;
            }
            for (int i = 0; i < workers.Count; i++)
            {
                var worker = workers[i];
                var _generationQuantity = CurrentGenerationQuantity;
                if (worker.hunger >= 1)
                {
                    _generationQuantity *= 0.1f; // if there is no food, reduce generation by 90%
                }
                if (worker.rest > 0)
                {
                    load += _generationQuantity;
                    worker.rest -= tiringRate;
                    worker.hunger = Math.Min(worker.hunger + hungerRate, 1);
                }
                else
                {
                    worker.rest = 0;
                    workers.RemoveAt(i);
                    GameObject p = Instantiate(
                        Prefabs.GetPrefab(Prefabs.UnitType.Person),
                        transform.position,
                        Quaternion.identity
                    );
                    PersonMovable pm = p.GetComponent<PersonMovable>();
                    pm.person = worker;
                    pm.isGoingHome = true;
                }
            }
            int total = (int)load;
            if (total > 0)
            {
                Manager.Resources.AddToResource(resource, total);
                if (newResourceUI != null)
                    newResourceUI.Show();
                remainingResources -= total;
                load -= total;
            }
            if (sliderBar != null)
            {
                sliderBar.ChangeColor(Color.blue);
                sliderBar.SetValue(remainingResources / (float)totalResources);
            }
            if (!isInfinite && remainingResources <= 0)
            {
                HandleEmptyState();
            }
        }
    }

    private void SendWorkerHome(Person worker)
    {
        GameObject p = Instantiate(
            Prefabs.GetPrefab(Prefabs.UnitType.Person),
            transform.position,
            Quaternion.identity
        );
        PersonMovable pm = p.GetComponent<PersonMovable>();
        pm.person = worker;
        pm.isGoingHome = true;
    }

    private void HandleEmptyState()
    {
        // Handle the state when the resource generator is empty
        foreach (var worker in workers)
        {
            GameObject p = Instantiate(
                Prefabs.GetPrefab(Prefabs.UnitType.Person),
                transform.position,
                Quaternion.identity
            );
            PersonMovable pm = p.GetComponent<PersonMovable>();
            pm.person = worker;
            pm.isGoingHome = true;
        }
        if (emptyPrefab != null)
        {
            Instantiate(emptyPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject.transform.parent.gameObject);
    }
}
