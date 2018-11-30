using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumpadHandler : MonoBehaviour
{
    const int outputLimit = 4;
    [SerializeField]
    List<GameObject> pads = new List<GameObject>(); //Assign em all in editor
    [SerializeField]
    TextMeshPro output;
    [SerializeField]
    GameObject previewText;

    [SerializeField]
    ShakeUI textShaker;
    [SerializeField]
    string code = "1234";

    [SerializeField]
    Material buttonPressMat;
    Material buttonStartMat;

    void Start()
    {
        //They all start with the same materials so the first one should be fine.
        buttonStartMat = pads[0].GetComponent<MeshRenderer>().material;
        ClearText();        
    }

    public void ButtonPressed(int padIndex)
    {
        previewText.SetActive(false);
        output.text += padIndex.ToString();

        if (output.text.Length >= outputLimit)
        {
            CheckAnswer();
        }

        pads[padIndex - 1].GetComponent<MeshRenderer>().material = buttonStartMat;
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


    public void ChangeMat(MeshRenderer mr)
    {
        mr.material = buttonPressMat;
    }
}
