using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine;

public class CodeDetection : MonoBehaviour
{
    public UnityEvent incorrectCalls;
    public UnityEvent correctCalls;

    [Header("The code needed to boot-up VR")]
    public string code = "1337";
    
    TMP_InputField inputField;
    [Header("A little delay before the code-based correct calls happen.")]
    [SerializeField]
    float correctCallDelay = 0.45f;

    VR_Lock vrLock;

    void Start()
    {
        vrLock = FindObjectOfType<VR_Lock>();
        inputField = GetComponent<TMP_InputField>();    

        if(GameObject.FindGameObjectWithTag("BuilderApp") != null)
        {
            //print(GameObject.FindGameObjectWithTag("BuilderApp").name);
            Transform t = GameObject.FindGameObjectWithTag("BuilderApp").transform.GetChild(1);            
            correctCalls.AddListener(t.GetComponent<SubMenuButton>().SubMenuToggle);
        }
    }

    public void CheckStringLenght(string s)
    {
        if(s.Length == 4)
        {
            CheckAnswer(s);
        }
    }

    void CheckAnswer(string s)
    {
        if(code == s)
        {
            Correct();
        }
        else
        {
            InCorrect();
        }
    }


    void Correct()
    {
        vrLock.ChangeLockState(true);

        if (correctCalls != null)
            correctCalls.Invoke();
    }

    void InCorrect()
    {        
        if(incorrectCalls != null)
            incorrectCalls.Invoke();        
    }

    public void DeleteText()
    {                
        inputField.text = "";        
    }
}