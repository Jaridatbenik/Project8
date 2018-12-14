using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField]
    List<char> allowedLetters = new List<char>();
    [SerializeField]
    List<char> allowedMathSyntaxes = new List<char>();
    string savedText = "";

    [Space]
    [SerializeField]
    TMP_InputField text;


    void Start()
    {
        GetText();
    }
    void GetText()
    {
        savedText = text.text;
    }

    public void PlayerInput()
    {
        string newText = text.text;

        if (newText.Length > savedText.Length)
        {
            //Added
            for (int i = savedText.Length; i < newText.Length; i++)
            {                
                if (!allowedLetters.Contains(newText[i]) && !allowedMathSyntaxes.Contains(newText[i]))
                {
                    text.text = newText.Substring(0, newText.Length - 1);
                }
                if (newText.Length > 1)
                {
                    //Make sure that you can't have double syntaxes like ++ or /+
                    if (allowedMathSyntaxes.Contains(newText[i]) && allowedMathSyntaxes.Contains(newText[i - 1]))
                    {
                        text.text = newText.Substring(0, newText.Length - 1);
                    }
                }
            }
        }


        GetText();
    }

    void Update()
    {
        if (text.isFocused && Input.GetButtonDown("Submit_Calculator"))
        {
            Submit();
        }
    }

    public void Submit()
    {        
        string firstPart = "";
        string secondPart = "";

        int firstSyntaxIndex = 0;
        char firstSyntax = ' ';        


        //GET SYNTAX
        for(int i = 0; i < allowedMathSyntaxes.Count; i++)
        {
            for(int f = 0; f < text.text.Length; f++)
            {
                if(text.text[f] == allowedMathSyntaxes[i])
                {
                    //When it finds a syntax, it'll save it.
                    firstSyntaxIndex = f + 1;
                    firstSyntax = text.text[f];
                }
            }
        }

        //GET PARTS
        //First
        for(int i = 0; i < firstSyntaxIndex - 1; i++)
        {            
            firstPart += text.text[i];
        }
        //Second
        for(int i = firstSyntaxIndex; i < text.text.Length; i++)
        {
            secondPart += text.text[i];
        }


        //print(firstPart + " | " + firstSyntax + " | " + secondPart);
        SYNTAX_Equals(firstPart, secondPart, firstSyntax);
    }

    public void SYNTAX_Equals(string first, string second, char syntax)
    {
        float string1 = float.Parse(first);
        float string2 = float.Parse(second);

        //print(string1 + syntax + string2);

        float answer = 0;

        if(string1.ToString().Length > 0 && string2.ToString().Length > 0 && allowedMathSyntaxes.Contains(syntax))
        {
            if (syntax == '+')
            {
                answer = string1 + string2;
            }
            else if (syntax == '-')
            {
                answer = string1 - string2;
            }
            else if (syntax == 'x' || syntax == '*')
            {
                answer = string1 * string2;
            }
            else if (syntax == '/')
            {
                answer = string1 / string2;
            }
        } 
        

        text.text = answer.ToString();
    }


    public void ClearText()
    {        
        text.text = "";        
    }

    public void InputButton(string c)
    {
        text.text += c;
    }
}