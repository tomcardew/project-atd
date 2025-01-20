using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public properties
    [Header("Waves configuration")]
    public float timeBeforeFirstWave = 30f;
    public float waveDuration;
    public float waveDurationMultiplier = 1.0f;
    public float waveRestDuration;
    public float waveRestDurationMultiplier = 1.0f;

    [Header("Main spawner configuration")]
    public float delay;
    public float delayMultiplier = 1.0f;
    public float quantity;
    public float quantityMultiplier = 1.0f;

    [Header("Resources configuration")]
    public int numberOfTrees;

    // Private properties
    [NonSerialized]
    public int currentRound = 0;

    private EnemyRoundSpawner mainSpawner;
    private LineController lineController;
    private Coroutine endGameRoutine;
    private Coroutine waveRoutine;

    public bool hasBeenStarted = false;
    public bool waitingForDraw = false;

    public float timeBeforeNextWave = 0;
    public float baseCountingTime = 0;
    public bool isOnRestTime = false;
    public bool isOnWave = false;

    public delegate void WaveEventHandler();
    public static event WaveEventHandler OnWaveStart;
    public static event WaveEventHandler OnWaveEnd;

    public float CurrentDelay
    {
        get { return delay * delayMultiplier; }
    }

    public float CurrentQuantity
    {
        get { return quantity * quantityMultiplier; }
    }

    public float CurrentWaveDuration
    {
        get { return waveDuration * waveDurationMultiplier; }
    }

    public float CurrentWaveRestDuration
    {
        get { return waveRestDuration * waveRestDurationMultiplier; }
    }

    private void Update()
    {
        if (timeBeforeNextWave > 0)
        {
            timeBeforeNextWave -= Manager.Time.pausableDeltaTime;
        }
    }

    private void OnDisable()
    {
        if (endGameRoutine != null)
        {
            StopCoroutine(endGameRoutine);
            endGameRoutine = null;
        }
        if (waveRoutine != null)
        {
            StopCoroutine(waveRoutine);
            waveRoutine = null;
        }
    }

    private IEnumerator InternalWaveRoutine()
    {
        baseCountingTime = timeBeforeFirstWave;
        timeBeforeNextWave = timeBeforeFirstWave;
        yield return new WaitForSeconds(timeBeforeFirstWave);
        while (true)
        {
            StartWave();
            yield return new WaitForSeconds(CurrentWaveDuration);
            EndWave();
            while (waitingForDraw)
            {
                yield return null;
            }
            yield return new WaitForSeconds(CurrentWaveRestDuration);
            currentRound++;
            isOnRestTime = false;
            isOnWave = true;
        }
    }

    private void StartWave()
    {
        isOnWave = true;
        OnWaveStart?.Invoke();
        mainSpawner.shouldSpawn = true;
        mainSpawner.currentRound = currentRound;
        baseCountingTime = CurrentWaveDuration;
        timeBeforeNextWave = CurrentWaveDuration;
    }

    private void EndWave()
    {
        mainSpawner.shouldSpawn = false;
        OnWaveEnd?.Invoke();
        baseCountingTime = CurrentWaveRestDuration;
        timeBeforeNextWave = CurrentWaveRestDuration;
        isOnWave = false;
        isOnRestTime = true;
        waitingForDraw = true;
    }

    private IEnumerator EndGameRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (hasBeenStarted && Manager.Population.GetPopulation() <= 0)
            {
                ExitGame();
            }
        }
    }

    public void StartGame()
    {
        waveRoutine = StartCoroutine(InternalWaveRoutine());
        endGameRoutine = StartCoroutine(EndGameRoutine());
        timeBeforeNextWave = timeBeforeFirstWave;
        InstantiateEssentials();
    }

    private void InstantiateEssentials()
    {
        Vector3 castlePosition = Utils.GetPositionOnBorder(Camera.main, 2f, 300f);
        GameObject castle = Instantiate(
            Prefabs.GetPrefab(Prefabs.StructureType.Castle),
            castlePosition,
            Quaternion.identity
        );
        Vector3 entrance = castle.GetComponent<CastleController>().GetEntrancePosition();

        Vector3 oppositePosition = Utils.GetOppositeCornerOutsideView(castlePosition, 3f);
        mainSpawner = CreateMainSpawner(oppositePosition);

        lineController = CreateLineDrawer(mainSpawner.gameObject.transform.position, entrance);
        GenerateResources();
    }

    private EnemyRoundSpawner CreateMainSpawner(Vector3 position)
    {
        GameObject spawner = new GameObject("MainSpawner");
        spawner.transform.position = position;

        EnemyRoundSpawner ctrl = spawner.AddComponent<EnemyRoundSpawner>();
        ctrl.initialDelay = CurrentDelay;
        ctrl.initialQuantity = (int)CurrentQuantity;
        ctrl.currentRound = currentRound;
        ctrl.isInfinite = true;
        ctrl.shouldSpawn = false;

        return ctrl;
    }

    private LineController CreateLineDrawer(Vector3 a, Vector3 b)
    {
        GameObject lineDrawer = Instantiate(Prefabs.GetPrefab(Prefabs.OtherType.LineDrawer));
        LineController ctrl = lineDrawer.GetComponent<LineController>();
        ctrl.start = a;
        ctrl.end = b;
        return ctrl;
    }

    private void GenerateResources()
    {
        List<GameObject> trees = new List<GameObject>
        {
            Prefabs.GetPrefab(Prefabs.ResourceType.SmallTree),
            Prefabs.GetPrefab(Prefabs.ResourceType.MediumTree),
            Prefabs.GetPrefab(Prefabs.ResourceType.LargeTree),
        };

        List<Vector3> generatedPositions = new List<Vector3>();

        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 position;
            do
            {
                position = Utils.GetRandomPositionInsideCamera(Camera.main, 3f);
            } while (
                IsPositionOnLine(position, lineController.start, lineController.end)
                || IsPositionTooClose(position, generatedPositions, 2f)
            );

            generatedPositions.Add(position);
            Instantiate(
                trees[UnityEngine.Random.Range(0, trees.Count)],
                position,
                Quaternion.identity
            );
        }
    }

    private bool IsPositionOnLine(Vector3 position, Vector3 lineStart, Vector3 lineEnd)
    {
        float distance = Vector3.Distance(lineStart, lineEnd);
        float distanceToStart = Vector3.Distance(position, lineStart);
        float distanceToEnd = Vector3.Distance(position, lineEnd);

        // Check if the position is close to the line within a small threshold
        return Mathf.Abs(distance - (distanceToStart + distanceToEnd)) < 1f;
    }

    private bool IsPositionTooClose(Vector3 position, List<Vector3> positions, float minDistance)
    {
        foreach (Vector3 pos in positions)
        {
            if (Vector3.Distance(position, pos) < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
