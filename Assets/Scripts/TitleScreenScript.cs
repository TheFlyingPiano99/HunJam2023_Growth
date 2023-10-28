using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public GameObject blackScreen;
    public float fadeInSpeed = 0.5f;

    void Start()
    {
        blackScreen.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    }

    void Update()
    {

        var alpha = 1.0f - Mathf.Min(Mathf.Pow(Time.time * fadeInSpeed, 3.0f), 1.0f);
        blackScreen.GetComponent<CanvasRenderer>().SetAlpha(alpha);

        if (alpha < 0.01f)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Starting 1th level.");
                SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Level01");
            }
        }
    }
}
