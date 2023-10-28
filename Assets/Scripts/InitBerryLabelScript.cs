using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitBerryLabelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var pickUpScript = gameObject.GetComponentInParent<PickingUpGrowthBerryScript>();
        var mode = pickUpScript.GetGrowMode();
        var param = pickUpScript.GetGrowthParameter();
        var textObj = GetComponent<TMP_Text>();
        switch (mode) {
            case PickingUpGrowthBerryScript.GrowMode.additive:
                textObj.text = "+" + param.ToString();
                break;
            case PickingUpGrowthBerryScript.GrowMode.multipicative:
                textObj.text = "x" + param.ToString();
                break;
        }
    }
}
