using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillingRoofScript : MonoBehaviour
{
    private GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
        if (gameController == null)
        {
            Debug.LogError("Controller is null!");
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player killed by overgrowing.");
            gameObject.GetComponent<AudioSource>().Play();
            gameController.GetComponent<GameControllerScript>().TriggerGameOver();
        }
    }
}
