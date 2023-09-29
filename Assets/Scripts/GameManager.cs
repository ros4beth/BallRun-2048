using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelPrefabs; 
    private GameObject currentLevelInstance;  
    private int currentLevel = 0;

    private void Start()
    {
        LoadLevel(currentLevel);
    }

    public void RestartGame()
    {
        Debug.Log("Restarting the game...");
        LoadLevel(currentLevel);
    }

    public void LoadNextLevel()
    {
        currentLevel++;

        if (currentLevel < levelPrefabs.Length)
        {
            LoadLevel(currentLevel);
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    private void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelPrefabs.Length)
        {
            if (currentLevelInstance != null)
            {
                Destroy(currentLevelInstance);
            }
            currentLevelInstance = Instantiate(levelPrefabs[levelIndex]);
        }
        else
        {
            Debug.LogError("Invalid level index: " + levelIndex);
        }
    }
}