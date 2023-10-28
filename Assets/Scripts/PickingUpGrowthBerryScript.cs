using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpGrowthBerryScript : MonoBehaviour
{
    public enum GrowMode
    {
        additive,
        multipicative
    };
    public GrowMode growMode;
    public int growthParamterer = 1; // to add or multipy with

    public GrowMode GetGrowMode()
    {
        return growMode;
    }

    public int GetGrowthParameter()
    {
        return growthParamterer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var script = other.gameObject.GetComponent<GrowthScript>();
            if (null != script)
            {
                switch (growMode) {
                    case GrowMode.additive:
                        script.Grow(growthParamterer, 1);
                        break;
                    case GrowMode.multipicative:
                        script.Grow(0, growthParamterer);
                        break;
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Collision with player but no GrowthScript found!");
            }
        }
    }
}
