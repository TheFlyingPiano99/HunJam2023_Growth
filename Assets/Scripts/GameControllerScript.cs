using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public enum GameState
    {
        None,
        GameOver,
        GamePaused,
        GameRunning,
        LevelFinished
    }

    private GameObject player;
    public GameObject gameOverScreen;
    public GameObject levelFinishScreen;
    public TMP_Text growthProgressText;
    public Slider growthProgressSlider;
    private GameState gameState;
    private double timeSpentInState = 0.0f;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        ResetLevel();
    }

    private void waitForInputAndRestartScene()
    {
        if (timeSpentInState >= 3.0f)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().name
                );
            }
        }
    }

    public void Update()
    {
        timeSpentInState += Time.deltaTime;
        switch (gameState)
        {
            case GameState.GameOver:
                waitForInputAndRestartScene();
                break;
            case GameState.GamePaused:

                break;
            case GameState.GameRunning:
                break;
            case GameState.LevelFinished:
                waitForInputAndRestartScene();
                break;
        }
    }

    public void ResetLevel()
    {
        gameState = GameState.GameRunning;
        gameOverScreen.SetActive(false);
        levelFinishScreen.SetActive(false);
    }

    public void UpdateGrowthProgressInfo(int size)
    {
        growthProgressText.text = "Growth: " + size.ToString();
    }

    public void TriggerGameOver()
    {
        if (GameState.GameOver == gameState) // Prevent multiple trigger if already in GameOver
            return;
        if (GameState.LevelFinished == gameState) // Prevent Gem Over when finished level
            return;

        gameState = GameState.GameOver;
        timeSpentInState = 0.0;
        gameOverScreen.SetActive(true);
        Destroy(player);
        Debug.Log("Game Over triggered.");
    }

    public void TriggerLevelFinish()
    {
        if (GameState.LevelFinished == gameState)    // Not able to win if game over
            return;
        timeSpentInState = 0.0;
        gameState = GameState.LevelFinished;
        levelFinishScreen.SetActive(true);
        Debug.Log("Level finished.");
    }

}
