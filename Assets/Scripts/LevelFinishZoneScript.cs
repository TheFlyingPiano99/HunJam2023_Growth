using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishZoneScript : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Level finished.");
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("GameController")
                .GetComponent<GameControllerScript>()
                .TriggerLevelFinish();
        }
    }
}
