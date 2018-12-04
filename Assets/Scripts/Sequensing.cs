using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequensing : MonoBehaviour {

    public int currentBodyPart = 0;

    public GameObject inputDing;
    ObjectPicker pick1;
    ObjectPicker_Unlocks pick2;

    private void Start()
    {
        pick1 = GetComponent<ObjectPicker>();
        pick2 = GetComponent<ObjectPicker_Unlocks>();
        pick1.enabled = true;
        pick2.enabled = false;
        inputDing.SetActive(false);
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
        if(currentBodyPart >= 4)
        {
            inputDing.SetActive(true);
            pick1.enabled = false;
            pick2.enabled = true;   
        }
    }


}
