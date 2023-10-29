using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpKeyScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Opendoor doorScript = GameObject.Find("DOOR").GetComponent<Opendoor>();
            if (null != doorScript)
            {
                var tuneSource = GameObject.Find("PickUpTune").GetComponent<AudioSource>();
                tuneSource.Play();
                doorScript.UnlockDoor();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Collision between key and player but no Dooropen script found!");
            }
        }
    }
}
