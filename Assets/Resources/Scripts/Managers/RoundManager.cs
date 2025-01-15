using UnityEngine;

public struct RoundEnemyWithWeight
{
    public GameObject enemy;
    public float weight;

    public RoundEnemyWithWeight(GameObject enemy, float weight)
    {
        this.enemy = enemy;
        this.weight = weight;
    }
}

public struct Round
{
    public float delay;
    public float quantity;
    public RoundEnemyWithWeight[] weightedEnemies;

    public Round(float delay, float quantity, RoundEnemyWithWeight[] weightedEnemies)
    {
        this.delay = delay;
        this.quantity = quantity;
        this.weightedEnemies = weightedEnemies;
    }
}

public class RoundManager : MonoBehaviour
{
    private int round = 0;
    private float delay;
    private float quantity;

    public void Init()
    {
        round = 0;
    }

    public Round GetNextRound()
    {
        return new Round(
            1,
            1,
            new RoundEnemyWithWeight[]
            {
                new RoundEnemyWithWeight(Prefabs.Enemy, 1f),
                new RoundEnemyWithWeight(Prefabs.LargeEnemy, 0.7f),
                new RoundEnemyWithWeight(Prefabs.Tank, 0.5f),
            }
        );
    }
}
