using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : MonoBehaviour
{
    public GameObject robot;
    int num = 2;

    public GameObject pointA;
    public GameObject pointB;
    public GameObject leverObject;
    bool atPointA = true;
    bool inSwitch = false;

    public float speed = 2f;

    void Start()
    {

    }

    public void SwitchLever()
    {
        inSwitch = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchLever();
        }

        if (inSwitch)
        {
            if (atPointA)
            {
                leverObject.transform.position = Vector3.Lerp(leverObject.transform.position, pointB.transform.position, Time.deltaTime * speed);
                if(Vector3.Distance(leverObject.transform.position, pointB.transform.position) < 0.02f)
                {
                    atPointA = false;
                    inSwitch = false;
                }
            }
            else
            {
                leverObject.transform.position = Vector3.Lerp(leverObject.transform.position, pointA.transform.position, Time.deltaTime * speed);
                if (Vector3.Distance(leverObject.transform.position, pointA.transform.position) < 0.02f)
                {
                    atPointA = true;
                    inSwitch = false;
                }
            }
        }
    }

}
