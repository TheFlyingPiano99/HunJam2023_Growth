using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opendoor : MonoBehaviour
{
    bool Isoppen =false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Open_Door()
    {
        float angle = 30 * Time.deltaTime;
        if(this.transform.rotation.eulerAngles.y<=90)
            this.transform.Rotate(0, angle,  0,Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Isoppen)
            Open_Door();
    }
}