using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public enum GameState
    {
        None,
        GameOver,
        GamePaused,
        GameRunning
    }

    private GameObject player;
    public TMP_Text gameOverText;
    public Slider growthProgressSlider;
    private GameState gameState;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameOverText.enabled = false;
    }

    public void UpdateGrowthProgressInfo(float interpolationVariable)
    {
        growthProgressSlider.value = interpolationVariable;
    }

    public void TriggerGameOver()
    {
        if (GameState.GameOver == gameState) // Prevent multiple trigger if already in GameOver
            return;
        gameState = GameState.GameOver;
        gameOverText.enabled = true;
        Destroy(player);
        Debug.Log("Game Over triggered.");
    }
}
