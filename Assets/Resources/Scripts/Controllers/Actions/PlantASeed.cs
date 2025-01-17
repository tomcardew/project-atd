using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantASeed : MonoBehaviour
{
    // Public properties
    public float durationBeforeGrowing;
    public float durationBeforeGrowingMultiplier = 1.0f;

    // Private properties
    private float currentTime;
    private SliderController loadingBar;
    private List<GameObject> resources;
    private Coroutine growRoutine;
    private float CurrentDurationBeforeGrowing =>
        durationBeforeGrowing * durationBeforeGrowingMultiplier;

    private void Start()
    {
        resources = new()
        {
            Prefabs.GetPrefab(Prefabs.ResourceType.SmallTree),
            Prefabs.GetPrefab(Prefabs.ResourceType.MediumTree),
            Prefabs.GetPrefab(Prefabs.ResourceType.LargeTree),
        };
        loadingBar = GetComponentInChildren<SliderController>();
        loadingBar.ChangeColor(Color.yellow);
        growRoutine = StartCoroutine(GrowRoutine());
        currentTime = 0f;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        var value = currentTime / CurrentDurationBeforeGrowing;
        loadingBar.SetValue(value);
    }

    private void OnDisable()
    {
        if (growRoutine != null)
        {
            StopCoroutine(growRoutine);
            growRoutine = null;
        }
    }

    private IEnumerator GrowRoutine()
    {
        yield return new WaitForSeconds(CurrentDurationBeforeGrowing);
        Grow();
    }

    private void Grow()
    {
        var resource = resources[Random.Range(0, resources.Count)];
        Instantiate(resource, transform.localPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
