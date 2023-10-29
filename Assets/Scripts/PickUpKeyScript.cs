using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpKeyScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var script = other.gameObject.GetComponent<GrowthScript>();
            if (null != script)
            {
                var tuneSource = GameObject.Find("PickUpTune").GetComponent<AudioSource>();
                tuneSource.Play();

                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Collision between key and player but no GrowthScript found!");
            }
        }
    }
}
