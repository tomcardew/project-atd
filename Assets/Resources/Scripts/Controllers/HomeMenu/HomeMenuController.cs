using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenuController : MonoBehaviour
{
    [Header("Map configuration")]
    public int numberOfTrees;

    [Header("Menu configuration")]
    public Button startButton;
    public Button databaseButton;
    public Button exitButton;
    public GameObject databasePanel;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        databaseButton.onClick.AddListener(OpenDatabase);
        exitButton.onClick.AddListener(ExitGame);

        databasePanel.SetActive(false);

        GenerateResources();
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void OpenDatabase()
    {
        databasePanel.SetActive(true);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Map");
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
                position = Utils.GetRandomPositionInsideCamera(Camera.main, 0f, 0f);
            } while (Utils.IsPositionTooClose(position, generatedPositions, 2f));

            generatedPositions.Add(position);
            Instantiate(
                trees[UnityEngine.Random.Range(0, trees.Count)],
                position,
                Quaternion.identity
            );
        }
    }
}
