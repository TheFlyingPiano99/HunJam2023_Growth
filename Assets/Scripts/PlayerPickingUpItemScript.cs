using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickingUpItemScript : MonoBehaviour
{

    public void pickUpItem(GameObject item)
    {
        Debug.Log("Item picked up: " + item.name);
    }

}
