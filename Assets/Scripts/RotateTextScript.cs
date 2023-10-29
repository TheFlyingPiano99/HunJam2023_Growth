using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTextScript : MonoBehaviour
{

    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.current.transform.rotation  * Vector3.forward,
            Camera.current.GetComponent<Camera>().transform.rotation * Vector3.up);
    }
}
