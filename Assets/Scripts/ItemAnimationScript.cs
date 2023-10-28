using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemAnimationScript : MonoBehaviour
{
    private Vector3 initialPosition; // without the offset created by animation
    private Quaternion initialRotation;
    public float animationAmplitude = 0.25f;
    public float oscillationAngularFrequency = 2.0f;
    public float rotationAngularFrequency = 1.0f;

    private 

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Hover animation:
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            initialPosition.y + animationAmplitude * Mathf.Sin(oscillationAngularFrequency * Time.time),
            gameObject.transform.position.z
        );

        gameObject.transform.Rotate(new Vector3(0, 1, 0), rotationAngularFrequency * Time.deltaTime);
    }
}
