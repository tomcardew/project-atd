using System.Collections;
using System.Linq;
using UnityEngine;

public class RepairShop : MonoBehaviour
{
    public float initialRepairRate = 1f;
    public float repairRateMultiplier = 1f;
    public float initialRepairDelay = 1f;
    public float repairDelayMultiplier = 1f;
    public int initialWoodConsumption = 1;
    public float woodConsumptionMultiplier = 1f;

    private Damageable currentDamageable;
    private Coroutine repairCoroutine;

    private float CurrentRepairRate
    {
        get { return initialRepairRate * repairRateMultiplier; }
    }

    private float CurrentRepairDelay
    {
        get { return initialRepairDelay * repairDelayMultiplier; }
    }

    private float CurrentWoodConsumption
    {
        get { return initialWoodConsumption * woodConsumptionMultiplier; }
    }

    private void Start()
    {
        StartCoroutine(RepairCoroutine());
    }

    private void OnDisable()
    {
        if (repairCoroutine != null)
        {
            StopCoroutine(repairCoroutine);
            repairCoroutine = null;
        }
    }

    private IEnumerator RepairCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(CurrentRepairDelay);
            if (currentDamageable == null)
            {
                currentDamageable = GetNextDamageable();
                continue;
            }

            Debug.Log("Repairing " + currentDamageable.gameObject.transform.parent.name);
            if (Manager.Resources.CanPay(ResourceType.Wood, (int)CurrentWoodConsumption))
            {
                Manager.Resources.AddToResource(ResourceType.Wood, -(int)CurrentWoodConsumption);
                currentDamageable.ReceiveDamage(-CurrentRepairRate);
                if (currentDamageable.receivedDamage <= 0)
                {
                    currentDamageable.receivedDamage = 0;
                    currentDamageable = GetNextDamageable();
                }
            }
        }
    }

    private Damageable GetNextDamageable()
    {
        return Utils
            .FindAllNearObjectsOfType<Damageable>(transform.position, float.MaxValue)
            .Where(d => d.repairable && d.receivedDamage > 0 && d.repairable)
            .OrderByDescending(d => d.receivedDamage)
            .FirstOrDefault();
    }
}
