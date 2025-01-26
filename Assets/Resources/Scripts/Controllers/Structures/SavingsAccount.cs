using UnityEngine;

public class SavingsAccount : MonoBehaviour
{
    public float initialPercent;
    public float percentMultiplier = 1f;

    private float CurrentPercent
    {
        get { return initialPercent * percentMultiplier; }
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
        Debug.Log($"Generated {value} money from savings account");
        Manager.Resources.AddToResource(ResourceType.Money, value);
    }
}
