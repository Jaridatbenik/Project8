using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHandler : MonoBehaviour
{
    [SerializeField]Animator roboParentAnim;
    [SerializeField]
    string faceAwayTrigger = "FaceAway";
    [SerializeField]
    string FaceTowardsTrigger = "FaceTowards";
    
    public void AllAttached()
    {
        roboParentAnim.SetTrigger(faceAwayTrigger);
    }
    public void CodeCorrect()
    {
        roboParentAnim.SetTrigger(FaceTowardsTrigger);
    }

    void Update()
    {
        //Debug
        if(Input.GetKeyDown(KeyCode.R))
        {
            AllAttached();
        }    
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            CodeCorrect();
        }
    }   
}