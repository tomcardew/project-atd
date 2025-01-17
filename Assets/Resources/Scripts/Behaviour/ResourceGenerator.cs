using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResourceGenerator : MonoBehaviour
{
    // Public properties
    public ResourceType resource; // wood, faith
    public int workerCapacity; // max workers supported
    public int totalResources; // max available resources to generate
    public bool isInfinite = false; // if the generator is infinite
    public float tiringRate = 0.1f; // how much workers get tired each generation
    public float initialGenerationQuantity; // how many per worker
    public float generationQuantityMultiplier = 1.0f;
    public float initialGenerationDelay; // how often to generate
    public float generationDelayMultiplier = 1.0f;
    public GameObject emptyPrefab;
    public SliderController sliderBar; // Reference to the health bar

    // Private properties
    private Resource _resource;
    private Coroutine generationCoroutine;
    private int remainingResources;
    private float load = 0;
    private NewResourceUIController newResourceUI;

    [SerializeField]
    private List<Person> workers;

    public bool HasCapacity
    {
        get { return workers.Count < workerCapacity; }
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
        generationCoroutine = StartCoroutine(GenerationCoroutine());
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
            yield return new WaitForSeconds(CurrentGenerationDelay);
            for (int i = 0; i < workers.Count; i++)
            {
                var worker = workers[i];
                if (worker.rest > 0)
                {
                    load += CurrentGenerationQuantity;
                    worker.rest -= tiringRate;
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
