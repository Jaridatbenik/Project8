using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Lock : MonoBehaviour
{
    Camera vrCam;
    List<GameObject> hands = new List<GameObject>();

    void Awake()
    {
        vrCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("Hands").Length; i++)
        {
            hands.Add(GameObject.FindGameObjectsWithTag("Hands")[i]);
        }
        ChangeLockState(false);   
    }

    public void ChangeLockState(bool b)
    {
        vrCam.enabled = b;

        for(int i = 0; i < hands.Count; i++)
        {
            //hands[i].SetActive(b);
            //(de)activate controller stuff here.
        }
    }
}