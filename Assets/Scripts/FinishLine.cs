using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
  private bool levelCompleted = false;
  public GameObject levelCompletedPanel;
  public Button nextLevelButton;
  public GameManager scriptReference;
  private void Start()
  {
    scriptReference = Object.FindAnyObjectByType<GameManager>();

    levelCompletedPanel.SetActive(false);
    if (nextLevelButton != null)
    {
      nextLevelButton.gameObject.SetActive(false);
    }

    if (nextLevelButton != null)
    {
      nextLevelButton.onClick.AddListener(LoadNextLevel);
    }
  }

  private void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.name == "Player" && !levelCompleted)
    {
      levelCompleted = true;


      levelCompletedPanel.SetActive(true);


      if (nextLevelButton != null)
      {
        nextLevelButton.gameObject.SetActive(true);
      }
    }
  }

  private void LoadNextLevel()
  {
    if (scriptReference != null)
    {
      scriptReference.LoadNextLevel();
    }
  }
}