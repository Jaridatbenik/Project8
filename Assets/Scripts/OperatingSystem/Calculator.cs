using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public List<char> allowedCharacters = new List<char>();
    string savedText = "";

    [SerializeField]
    TMP_InputField text;

    void Start()
    {
        GetText();
    }
    public void PlayerInput()
    {
        string newText = text.text;
        print(newText + " - " + savedText);

        if (newText.Length > savedText.Length)
        {
            //Added
            for(int i = savedText.Length; i < savedText.Length; i++)
            {
                print("changed: " + newText[i]);
                if(!allowedCharacters.Contains(newText[i]))
                {
                    newText = newText.Substring(0, newText.Length - 1);
                }
            }
        }                        


        GetText();
    }

    void GetText()
    {
        savedText = text.text;
    }
}
