using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    public GameObject player;           // Assign the player GameObject in the Inspector
    private Vector3 playerStartPos = Vector3.zero; // Start position for player at (0,0,0)
    public float difficulty = 1f;       // Starting difficulty level

    private int gameLevel = 1;
    private bool isGameActive = false;

    public SpawnManager spawnManager;
    public PlayerController playerController;
    public ScoreManager scoreManager;
    public CountdownTimer timer;

    public GameObject pauseMenuUI; // Drag the pause menu UI Panel here in the Inspector
    private bool isPaused = false;
    public GameObject pauseButton;
    public GameObject mainMenu;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }


    public void InitializeGame()
    {
        Time.timeScale = 1f;
        spawnManager.SpawnObjects(0);
        mainMenu.SetActive(false);
        timer.StartCountdown();
        gameLevel = 1;
        difficulty = 1f;
        isGameActive = true;
        scoreManager.ResetScore();
        pauseButton.SetActive(true);
        playerController.PlayStartSound();

        Debug.Log("Game Started! Level: " + gameLevel);
    }

    public void ResetPlayer()
    {
        player.transform.position = playerStartPos;
        player.transform.rotation = Quaternion.identity;
    }

    public void IncreaseDifficulty()
    {
        spawnManager.ClearAllPrefabs();
        gameLevel++;
        spawnManager.SpawnObjects(gameLevel);
        Debug.Log("Level: " + gameLevel);
        timer.ResetTimer();
        timer.StartCountdown();
        ResetPlayer();
    }

    public void StartGame()
    {
        InitializeGame();
    }

    public void GameOver()
    {
        isGameActive = false;
        // Stop time to freeze the game
        Time.timeScale = 0f;
        mainMenu.SetActive(true);
        scoreManager.ResetScore();
        spawnManager.ClearAllPrefabs();
        playerController.ResetPlayer();
        ResetPlayer();
        // Optionally display a Game Over message
        Debug.Log("Game Over");
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f; // Resume game
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f; // Freeze game
        isPaused = true;
    }
}
