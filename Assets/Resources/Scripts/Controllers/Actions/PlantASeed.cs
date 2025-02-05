using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantASeed : MonoBehaviour
{
    // Public properties
    public bool randomResource = false;
    public Prefabs.ResourceType actionType = Prefabs.ResourceType.SmallTree;
    public float durationBeforeGrowing;
    public float durationBeforeGrowingMultiplier = 1.0f;

    // Private properties
    private float currentTime;
    private SliderController loadingBar;
    private List<GameObject> resources;
    private GameObject resource;
    private Coroutine growRoutine;
    private float CurrentDurationBeforeGrowing =>
        durationBeforeGrowing * durationBeforeGrowingMultiplier;

    private void Start()
    {
        resource = Prefabs.GetPrefab(actionType);
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
        currentTime += Manager.Time.pausableDeltaTime;
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
        GameObject _res;
        if (randomResource)
        {
            _res = resources[Random.Range(0, resources.Count)];
        }
        else
        {
            _res = resource;
        }
        Instantiate(_res, transform.localPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
