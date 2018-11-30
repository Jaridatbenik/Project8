using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : MonoBehaviour
{
    Animator lever;
    public GameObject robot;
    int num = 2;

    void Start()
    {
        lever = GetComponent<Animator>();
    }

    public void SwitchLever()
    {
        num = num == 1 ? 2 : 1;
        lever.SetTrigger("Play" + num);
        //lever.ResetTrigger("Play" + num);
        //robot.transform.rotation = num == 1 ? new Quaternion(0, 90, 0, 0) : new Quaternion(0, -90, 0, 0);

        if(num == 1)
        {
            robot.transform.rotation = Quaternion.Euler(0, 90, 0);
        }else if(num == 2)
        {
            robot.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchLever();
        }
    }

}
