using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Include the TextMeshPro namespace


public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Reference to the TextMeshProUGUI component
    public float countdownDuration = 30f;  // Starting time in seconds
    private float currentTime;
    private bool isCountingDown = false;  // To track if the timer is running

    public GameManager gameManager;

    void Start()
    {
        // Initialize the timer without starting it yet
        currentTime = countdownDuration;
        UpdateCountdownText(currentTime);
    }

    void Update()
    {
        // Only count down if the timer is running
        if (isCountingDown && currentTime > 0)
        {
            currentTime -= Time.deltaTime;  // Decrease time by delta time each frame
            UpdateCountdownText(currentTime);
        }
        else if (currentTime <= 0)
        {
            currentTime = 0;  // Stop at 0
            UpdateCountdownText(currentTime);
            isCountingDown = false;  // Stop counting down
            gameManager.GameOver();
        }
    }

    // Method to start the countdown
    public void StartCountdown()
    {
        currentTime = countdownDuration;  // Reset the timer to the starting value
        isCountingDown = true;  // Begin counting down
        UpdateCountdownText(currentTime);  // Immediately update the display
    }

    // Method to update the countdown text
    void UpdateCountdownText(float time)
    {
        int seconds = Mathf.FloorToInt(time);  // Get seconds (no need for minutes in this case)
        countdownText.text = string.Format("Timer: {0}", seconds);  // Format as "Timer: 30", "Timer: 29", etc.
    }
    // Method to reset the timer
    public void ResetTimer()
    {
        currentTime = countdownDuration;  // Reset the timer to the initial countdown value
        isCountingDown = false;  // Stop the countdown (if it's running)
        UpdateCountdownText(currentTime);  // Update the display to show the full timer again
        Debug.Log("Timer reset!");
    }
}

