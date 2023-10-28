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
        LevelFinished,
    }

    private GameObject player;
    public GameObject gameOverScreen;
    public GameObject levelFinishScreen;
    public TMP_Text growthProgressText;
    public GameObject blackScreen;
    private GameState gameState;
    private double timeSpentInState = 0.0f;
    bool isFadeInFromBlack = true;
    public float fadeInSpeed = 2.0f;
    public AudioSource winAudio;
    public AudioSource gameOverAudio;
    public AudioSource ambientMusic;

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
        if (isFadeInFromBlack) {
            fadeInFromBlack();
        }
    }

    public void ResetLevel()
    {
        gameState = GameState.GameRunning;
        gameOverScreen.SetActive(false);
        levelFinishScreen.SetActive(false);
        blackScreen.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        isFadeInFromBlack = true;
        winAudio.Stop();
        gameOverAudio.Stop();
        ambientMusic.Play();
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
