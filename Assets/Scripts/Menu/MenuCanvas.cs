using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    SubMenu countdownMenu;
    Animator anim;
    public Transform countDownText;
       
    public void EVENT_ToggleAnim()
    {
        if (countdownMenu == null)
        {
            countdownMenu = FindObjectOfType<CountDown>().GetComponent<SubMenu>();
        }
        countdownMenu.Toggle();
    }
}