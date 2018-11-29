using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class NumPad_Button : MonoBehaviour
{
    [SerializeField]
    UnityEvent pressDown;
    [SerializeField]
    UnityEvent pressUp;
    [SerializeField]
    UnityEvent hover;

    public void OnPressDown()
    {
        //Change Color

        pressDown.Invoke();
    }
    public void OnPressUp()
    {
        pressUp.Invoke();
    }

    public void OnHover()
    {
        hover.Invoke();
    }
}