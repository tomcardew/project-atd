using UnityEngine;

public class SavingsAccount : MonoBehaviour
{
    public float initialPercent;
    public float percentMultiplier = 1f;

    private NewResourceUIController newResourceUI;

    private float CurrentPercent
    {
        get { return initialPercent * percentMultiplier; }
    }

    private void Start()
    {
        newResourceUI = transform.GetComponentInChildren<NewResourceUIController>();
        newResourceUI.resource = ResourceType.Money;
    }

    private void OnEnable()
    {
        GameManager.OnWaveEnd += OnWaveEnd;
    }

    private void OnDisable()
    {
        GameManager.OnWaveEnd -= OnWaveEnd;
    }

    private void OnWaveEnd()
    {
        var value = (int)(Manager.Resources.GetResourceValue(ResourceType.Money) * CurrentPercent);
        Manager.Resources.AddToResource(ResourceType.Money, value);
        newResourceUI.Show();
    }
}
