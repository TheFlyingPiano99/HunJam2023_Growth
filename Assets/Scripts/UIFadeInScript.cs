using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeInScript : MonoBehaviour
{
    private bool isFinished = false;
    private float timeEllapsed = 0.0f;
    public float startTime = 0.5f;
    public float finishTime = 4.0f;
    public float interpolationExponent = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        timeEllapsed = 0.0f;
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
            return;

        timeEllapsed += Time.deltaTime;
        if (startTime < timeEllapsed)
        {
            var t = Mathf.Min((timeEllapsed - startTime) / (finishTime - startTime), 1.0f);
            var alpha = Mathf.Pow(t, interpolationExponent);

            gameObject.GetComponent<CanvasRenderer>().SetAlpha(alpha);
            if (timeEllapsed >= finishTime)
            {
                isFinished = true;
            }
        }

    }
}
