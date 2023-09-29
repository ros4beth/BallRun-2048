using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  public TextMeshProUGUI scoreText;
  private int score = 0;
  private CharacterController controller;
  private Vector3 forwardDirection;
  private Vector3 lateralDirection;
  private Vector3 targetPosition;
  public float forwardSpeed;
  public float lateralSpeed;
  public GameObject gameOverPanel;
  public Button gameOverButton;
  public bool isGameOver = false;
  [SerializeField] private GameObject tapToPlayUI;
  public bool gameStarted = false;
  private bool reachedFinishLine = false;
  private bool isLevelComplete = false;
  private int currentLevel;
  public GameManager scriptReference;

  private void Start()
  {
    controller = GetComponent<CharacterController>();
    forwardDirection = Vector3.forward * forwardSpeed;
    targetPosition = transform.position;

    gameOverPanel.SetActive(false);

    currentLevel = SceneManager.GetActiveScene().buildIndex;

    // Show the "Tap to Play" UI for Level 1
    if (currentLevel == 0 && tapToPlayUI != null)
    {
      tapToPlayUI.SetActive(true);
    }
    else
    {
      // For other levels, start the game automatically
      gameStarted = true;
    }
    scriptReference = Object.FindAnyObjectByType<GameManager>();

    gameOverButton.onClick.AddListener(GameOverMethod);
    
  }

  void Update()
  {
    if (!isGameOver && gameStarted)
    {
      // Left and right movement
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        // move the player laterally
        targetPosition = new Vector3(hit.point.x, transform.position.y, transform.position.z);
      }

      // Move the player forward
      controller.Move(forwardDirection * Time.deltaTime);

      // Move the player laterally towards the target position
      float lateralMovement = Mathf.Clamp(targetPosition.x - transform.position.x, -1f, 1f) * lateralSpeed;
      controller.Move(new Vector3(lateralMovement, 0, 0) * Time.deltaTime);

      if (reachedFinishLine)
      {
        StopPlayerAfterDelay(1.0f);
      }
    }
    controller.center = controller.center;
  }

  private void StopPlayerAfterDelay(float delay)
  {
    StartCoroutine(StopPlayerCoroutine(delay));
  }

  private IEnumerator StopPlayerCoroutine(float delay)
  {
    yield return new WaitForSeconds(delay);

    gameStarted = false;

    isLevelComplete = true;

    reachedFinishLine = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("FinishLine") && !isLevelComplete)
    {
      Debug.Log("Player reached the finish line");
      LevelComplete();

      reachedFinishLine = true;
    }

  }
  public void StartGame()
  {
    // Hide the "Tap to Play" UI
    tapToPlayUI.SetActive(false);

    score = 0;
    scoreText.text = "0";
    gameStarted = true;
  }

  public void IncreaseScore()
  {
    score++;
    scoreText.text = score.ToString();
  }

  public void GameOver()
  {
    isGameOver = true;
    gameOverPanel.SetActive(true);
  }

  public void GameOverMethod()
  {
    scriptReference.RestartGame();
  }

  private void LevelComplete()
  {
    gameStarted = true;
  }
}