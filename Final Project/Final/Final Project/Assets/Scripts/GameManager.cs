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

    public void InitializeGame()
    {
        gameLevel = 1;
        difficulty = 1f;
        ResetPlayer();
        isGameActive = true;
        Debug.Log("Game Started! Level: " + gameLevel);
    }

    public void ResetPlayer()
    {
        player.transform.position = playerStartPos;
        player.transform.rotation = Quaternion.identity;
    }

    public void IncreaseDifficulty()
    {
        gameLevel++;
        spawnManager.SpawnObjects(gameLevel);
        Debug.Log("Level: " + gameLevel);
        ResetPlayer();
    }

    public void StartGame()
    {
        isGameActive = true;
        InitializeGame();
    }

    public void EndGame()
    {
        isGameActive = false;
        Debug.Log("Game Over!");
    }
}
