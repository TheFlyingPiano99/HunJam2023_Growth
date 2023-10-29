using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTextScript : MonoBehaviour
{

    void LateUpdate()
    {
        var cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("No camera!");
        }
        transform.LookAt(
            transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up
        );
    }
}
