using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequensing : MonoBehaviour {

    public int currentBodyPart = 0;

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
        if(currentBodyPart >= 4)
        {

        }
    }


}
