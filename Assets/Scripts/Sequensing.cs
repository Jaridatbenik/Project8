using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequensing : MonoBehaviour {

    public int currentBodyPart = 0;

    public GameObject inputDing;
    ObjectPicker pick1;
    ObjectPicker_Unlocks pick2;

    float timer = 0;

    private void Start()
    {
        pick1 = GetComponent<ObjectPicker>();
        pick2 = GetComponent<ObjectPicker_Unlocks>();
        pick1.enabled = true;
        pick2.enabled = false;
        //inputDing.SetActive(false);
    }

    public void UpCount()
    {
        currentBodyPart++;
    }

    public void UpCount(int input)
    {
        currentBodyPart+= input;
    }

    void Update()
    {
        try
        {
            if (currentBodyPart >= 9)
            {
                inputDing.SetActive(true);
                pick1.enabled = false;
                pick2.enabled = true;
            }
            else
            {
                inputDing.SetActive(false);
                pick1.enabled = true;
                pick2.enabled = false;
            }
        }
        catch { }

        List<SpoelHandler> handhand = new List<SpoelHandler>();
        handhand.Clear();

        if(currentBodyPart == 7)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                foreach (SpoelHandler hand in FindObjectsOfType<SpoelHandler>())
                {
                    if (hand.canBeEndPoint && !hand.canBeAStartingPoint)
                    {
                        handhand.Add(hand);
                    }
                }

                bool hasFailed = false;

                foreach (SpoelHandler hand in handhand)
                {
                    if (hand.kleurtje != hand.isColor && !hand.hasCableAttached)
                    {
                        hasFailed = true;
                    }
                }
                if (!hasFailed)
                {
                    Debug.Log("All are correct now");
                    currentBodyPart = 8;
                }
            }
        }
    }


}
