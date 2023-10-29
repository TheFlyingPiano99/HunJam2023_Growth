using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameControllerScript : MonoBehaviour
{
    public enum GameState
    {
        None,
        GameOver,
        GamePaused,
        GameRunning,
        LevelFinished,
    }

    private GameObject player;
    public GameObject gameOverScreen;
    public GameObject levelFinishScreen;
    public GameObject pausedScreen;
    public TMP_Text growthProgressText;
    public GameObject blackScreen;
    private GameState gameState;
    private double timeSpentInState = 0.0f;
    bool isFadeInFromBlack = true;
    public float fadeInSpeed = 2.0f;
    public AudioSource winAudio;
    public AudioSource gameOverAudio;
    public AudioSource ambientMusic;
    public GameObject roomPrefab;
    public GameObject playerPrefab;

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
                var levelName = SceneManager.GetActiveScene().name;
                SceneManager.UnloadScene(levelName);
                SceneManager.LoadScene(levelName);
            }
        }
    }

    private void checkInputInPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.GameRunning;
            Time.timeScale = 1.0f;
            pausedScreen.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 1.0f;
            pausedScreen.SetActive(false);
            Application.Quit();
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
                checkInputInPause();
                break;
            case GameState.GameRunning:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    transitionToPaused();
                }
                break;
            case GameState.LevelFinished:
                waitForInputAndRestartScene();
                break;
        }
        if (isFadeInFromBlack) {
            fadeInFromBlack();
        }
    }

    public void ResetLevel()
    {
        gameState = GameState.GameRunning;
        gameOverScreen.SetActive(false);
        levelFinishScreen.SetActive(false);
        pausedScreen.SetActive(false);
        blackScreen.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        isFadeInFromBlack = true;
        winAudio.Stop();
        gameOverAudio.Stop();
        ambientMusic.Play();
        Instantiate(roomPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(playerPrefab, new Vector3(0.0f, 1.5f, 0.0f), Quaternion.identity);
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
        gameOverAudio.Play();
        ambientMusic.Stop();
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
        ambientMusic.Stop();
        winAudio.Play();
        Debug.Log("Level finished.");
    }

    public void transitionToPaused()
    {
        if (gameState == GameState.GamePaused)
            return;

        gameState = GameState.GamePaused;
        timeSpentInState = 0.0;
        Time.timeScale = 0.0f;
        pausedScreen.SetActive(true);
        Debug.Log("Paused.");
    }

    private void fadeInFromBlack()
    {
        var alpha = 1.0f - Mathf.Min(Mathf.Pow(Time.time * fadeInSpeed, 2.0f), 1.0f);
        blackScreen.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        if (alpha == 0.0f)
        {
            isFadeInFromBlack = false;
            blackScreen.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        }
    }

}
