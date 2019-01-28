using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequensing : MonoBehaviour {

    public int currentBodyPart = 0;

    public GameObject inputDing;

    float timer = 0;

    public GameObject spoel;

    private void Start()
    {

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
        spoel.SetActive(false);
        try
        {
            if (currentBodyPart >= 9)
            {
                inputDing.SetActive(true);
            }
            else
            {
                inputDing.SetActive(false);
            }
        }
        catch { }

        List<SpoelHandler> handhand = new List<SpoelHandler>();
        handhand.Clear();

        if(currentBodyPart == 7)
        {
            spoel.SetActive(true);
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                foreach (SpoelHandler hand in FindObjectsOfType<SpoelHandler>())
                {
                    if (hand.canBeEndPoint && !hand.canBeAStartingPoint)
                    {
                        Debug.Log("found");
                        handhand.Add(hand);
                    }
                }

                bool hasFailed = false;

                foreach (SpoelHandler hand in handhand)
                {
                    if (hand.kleurtje != hand.isColor || !hand.hasCableAttached)
                    {
                        Debug.Log("should fail");
                        hasFailed = true;
                    }
                }
                if (!hasFailed)
                {
                    currentBodyPart = 8;
                }
            }
        }
    }
}
