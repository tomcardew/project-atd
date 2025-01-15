using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    private int currentRound = 0;
    private SpawnerController mainSpawner;
    private LineController lineController;
    private Coroutine endGameRoutine;
    private Coroutine waveRoutine;

    public float timeBeforeNextWave = 0;
    public float baseCountingTime = 0;

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

    private void Awake()
    {
        waveRoutine = StartCoroutine(InternalWaveRoutine());
        endGameRoutine = StartCoroutine(EndGameRoutine());
        timeBeforeNextWave = timeBeforeFirstWave;
    }

    private void Update()
    {
        if (timeBeforeNextWave > 0)
        {
            timeBeforeNextWave -= Time.deltaTime;
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
            OnWaveStart?.Invoke();
            AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/round-start"),
                Camera.main.transform.position,
                2f
            );
            mainSpawner.shouldGenerate = true;
            mainSpawner.currentWave = currentRound;
            yield return new WaitForSeconds(CurrentWaveDuration);
            mainSpawner.shouldGenerate = false;
            OnWaveEnd?.Invoke();
            currentRound++;
            baseCountingTime = CurrentWaveRestDuration;
            timeBeforeNextWave = CurrentWaveRestDuration;
            yield return new WaitForSeconds(CurrentWaveRestDuration);
        }
    }

    private IEnumerator EndGameRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Manager.Population.GetPopulation() <= 0)
            {
                Debug.Log("Game Over!");
                ExitGame();
            }
        }
    }

    public void StartGame()
    {
        InstantiateEssentials();
    }

    private void InstantiateEssentials()
    {
        Vector3 castlePosition = Utils.GetPositionOnBorder(Camera.main, 2f, 300f);
        GameObject castle = Instantiate(Prefabs.Castle, castlePosition, Quaternion.identity);

        Vector3 oppositePosition = Utils.GetOppositeCornerOutsideView(castlePosition, 3f);
        mainSpawner = CreateMainSpawner(oppositePosition);

        lineController = CreateLineDrawer(mainSpawner.gameObject, castle);
        GenerateResources();
    }

    private SpawnerController CreateMainSpawner(Vector3 position)
    {
        GameObject spawner = Instantiate(Prefabs.Spawner, position, Quaternion.identity);
        SpawnerController ctrl = spawner.GetComponent<SpawnerController>();
        ctrl.prefabs = Enemies.All.Select(unit => unit.prefab).ToArray();
        ctrl.delay = CurrentDelay;
        ctrl.quantity = (int)CurrentQuantity;
        ctrl.infiniteObjects = true;
        ctrl.shouldGenerate = false; // wait to start generating
        ctrl.spawnRadius = 0.5f;
        ctrl.useEnemySpawnConfiguration = true;
        return ctrl;
    }

    private LineController CreateLineDrawer(GameObject a, GameObject b)
    {
        GameObject lineDrawer = Instantiate(Prefabs.LineDrawer);
        LineController ctrl = lineDrawer.GetComponent<LineController>();
        ctrl.start = a;
        ctrl.end = b;
        return ctrl;
    }

    private void GenerateResources()
    {
        List<GameObject> trees = new List<GameObject>
        {
            Prefabs.LargeTree,
            Prefabs.MediumTree,
            Prefabs.SmallTree
        };

        List<Vector3> generatedPositions = new List<Vector3>();

        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 position;
            do
            {
                position = Utils.GetRandomPositionInsideCamera(Camera.main, 3f);
            } while (
                IsPositionOnLine(
                    position,
                    lineController.start.transform.position,
                    lineController.end.transform.position
                ) || IsPositionTooClose(position, generatedPositions, 1f)
            );

            generatedPositions.Add(position);
            Instantiate(trees[Random.Range(0, trees.Count)], position, Quaternion.identity);
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
        Debug.Log("Exiting game...");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
