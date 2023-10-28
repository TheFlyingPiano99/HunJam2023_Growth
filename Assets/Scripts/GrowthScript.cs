using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthScript : MonoBehaviour
{
    private int size = 1; // Current size
    private int goalSize = 1; // Size to grow to during the transition phase
    private bool isUnderTransition = false; // Is in transition phase?
    private float transitionStartSize = 1.0f;
    private float transitionInterpolator = 0.0f;
    private Vector3 originalLocalScale;
    public float growthSpeed = 1.0f;
    private GameObject gameController;

    public void Start()
    {
        originalLocalScale = gameObject.transform.localScale;
        gameController = GameObject.FindWithTag("GameController");
    }
    public void ResetSize(int s)
    {
        size = 1;
        goalSize = 1;
        transitionStartSize = 1.0f;
    }

    public float GetGrowthSpeed()
    {
        return growthSpeed;
    }

    public void SetGrowthSpeed(float speed)
    {
        growthSpeed = speed;
    }

    public float GetInterpolatedSize()
    {
        return (1.0f - transitionInterpolator) * transitionStartSize
            + transitionInterpolator * goalSize;
    }

    public float DerivalOfInterpolation()
    {
        return (goalSize - transitionStartSize) * growthSpeed;
    }

    public void Grow(int additive, int multiplicative)
    {
        Debug.Log("Starting growth");
        // If already under transition so have to change
        // to start size to prevent jump in interpolation
        transitionStartSize = GetInterpolatedSize();
        transitionInterpolator = 0.0f;
        isUnderTransition = true;

        goalSize *= multiplicative;
        goalSize += additive;
        gameController.GetComponent<GameControllerScript>().UpdateGrowthProgressInfo(goalSize);
    }

    public void PauseGrowth()
    {
        isUnderTransition = false;
    }

    public void ResumeGrowth()
    {
        isUnderTransition = false;
    }

    public bool isGrowing()
    {
        return isUnderTransition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnderTransition)
        {
            transitionInterpolator += Time.deltaTime * growthSpeed;
            if (1.0f <= transitionInterpolator)
            {
                isUnderTransition = false;
                transitionInterpolator = 1.0f;
                size = goalSize;
                transitionStartSize = size;
                Debug.Log("Growed to new size.");
            }
            gameObject.transform.position += new Vector3(0.0f, originalLocalScale.y * DerivalOfInterpolation() * Time.deltaTime, 0.0f);
            gameObject.transform.localScale = GetInterpolatedSize() * originalLocalScale;
        }
        
    }
}
