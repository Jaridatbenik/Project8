using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInput : MonoBehaviour {

    MegaHandler megaHand;

    void Start()
    {
        megaHand = FindObjectOfType<MegaHandler>();
    }

	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Button")
        {
            megaHand.MakeButtonBlue(megaHand.GetChildNum(col.gameObject));
        }
        if (col.gameObject.tag == "EpicButton")
        {
            megaHand.CheckIfCorrect();
        }
        if (col.gameObject.tag == "UltimateHendel")
        {
            if (!megaHand.canRotate)
            {
                megaHand.DraaiRobot();
            }
        }
        if (col.gameObject.tag == "UltimateReset")
        {
            megaHand.ResetRobot();
        }
    }
}
