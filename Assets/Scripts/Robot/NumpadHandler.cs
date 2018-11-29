using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumpadHandler : MonoBehaviour
{
    const int outputLimit = 4;
    List<GameObject> pads = new List<GameObject>();
    [SerializeField]
    TextMeshProUGUI output;
    [SerializeField]
    GameObject previewText;

    [SerializeField]ShakeUI textShaker;

    [SerializeField] string code = "1234";

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            pads.Add(transform.GetChild(i).gameObject);
        }         
    }

    public void ButtonPressed(int padIndex)
    {
        previewText.SetActive(false);
        output.text += padIndex.ToString();

        if (output.text.Length >= outputLimit)
        {
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        if (output.text == code)        
            Correct();        
        else
            InCorrect();
    }

    void Correct()
    {

    }

    void InCorrect()
    {
        textShaker.GoShake();
        //maybe shake robot(?)
    }

    public void ClearText()
    {
        output.text = "";
        previewText.SetActive(true);
    }
}
