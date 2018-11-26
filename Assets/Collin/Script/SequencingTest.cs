using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencingTest : MonoBehaviour
{
    public enum Step
    {
        First_step,
        Second_step,
        Third_step,
        Fourth_step,
        Fifth_step,
    }
    
    public Step currentState;

    public void ChangeSequence()
    {
        switch (currentState)
        {
            case Step.First_step:
                Debug.Log("First Step");
                break;
            case Step.Second_step:
                Debug.Log("Second Step");
                break;
            case Step.Third_step:
                Debug.Log("Third Step");
                break;
            case Step.Fourth_step:
                Debug.Log("Fourth Step");
                break;
            case Step.Fifth_step:
                Debug.Log("Fifth Step");
                break;
        }
    }
}
