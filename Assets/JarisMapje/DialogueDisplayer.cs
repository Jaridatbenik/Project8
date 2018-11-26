using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplayer : MonoBehaviour {

    public GameObject panel;
    public Text debugText;
    public List<Button> answerButtons; 

    Dialogue dialogue;
    string[] nextNode;

    int currentLetter = 0;
    bool hasToTick = false;
    string globalDialogue;
    bool canAcceptInput = false;

    bool hasDialogue = false;
    bool isQuestion = false;

    int answer = 0;
    public static int textSpeed = 2;

	void Start () {
        EndDialogue();
        DialogueBuffer.dialogueDisplayer = this;	
	}

	public void DisplayDialogue(Dialogue log)
    {
        EndDialogue();
        hasDialogue = true;
        dialogue = log;
        answer = 0;
        nextNode = new string[1] { dialogue.startID };
        CalculateWhatToDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ReceivedInput();
        }
    }


    public void ReceivedInput()
    {
        if (hasDialogue && canAcceptInput && !isQuestion)
        {
            answer = 0;
            CalculateWhatToDisplay();
        }
    }

    public void ReceivedInput(int input)
    {
        if (hasDialogue && canAcceptInput && isQuestion)
        {
            answer = input;
            CalculateWhatToDisplay();
        }
    }

    public void CalculateWhatToDisplay()
    {
        SetButtonsInactive();
        panel.SetActive(false);
        for (int i = 0; i < dialogue.dialogue.Count; i++)
        {
            if (dialogue.dialogue[i].GetType() == typeof(Sentence))
            {
                if (((Sentence)dialogue.dialogue[i]).id == nextNode[answer])
                {
                    nextNode = new string[1] { "" };
                    PushScentenceToDisplay(((Sentence)dialogue.dialogue[i]).scentence);
                    nextNode[0] = ((Sentence)dialogue.dialogue[i]).nextNode;
                    isQuestion = false;
                    return;
                }
            }
            else
            {
                if (((Question)dialogue.dialogue[i]).id == nextNode[answer])
                {
                    nextNode = new string[1] { "" };
                    PushScentenceToDisplay(((Question)dialogue.dialogue[i]).question);
                    PushAnswersToDisplay(((Question)dialogue.dialogue[i]).answers);
                    nextNode = ((Question)dialogue.dialogue[i]).nodes.ToArray();
                    isQuestion = true;
                    return;
                }
            }
        }

        EndDialogue();
    }

    public void EndDialogue()
    {
        debugText.text = "";
        isQuestion = false;
        hasToTick = false;
        canAcceptInput = false;
        globalDialogue = "";
        dialogue = null;
        currentLetter = 0;
        hasDialogue = false;
        nextNode = new string[0];
        SetButtonsInactive();
        panel.SetActive(false);

    }

    public void PushScentenceToDisplay(string dialog)
    {
        SetupToPush(false);
        globalDialogue = Reverse(dialog);
    }

    public void PushAnswersToDisplay(List<string> answers)
    {
        SetupToPush(true);
        SetButtonsActive(answers);
    }

    void SetupToPush(bool setpanelactive)
    {
        SetButtonsInactive();
        panel.SetActive(setpanelactive);
        hasToTick = true;
        canAcceptInput = false;
    }

    public void SetButtonsInactive()
    {
        for(int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].gameObject.SetActive(false);
        }
    }

    public void SetButtonsActive(List<string> answers)
    {
        for(int i = 0; i < answers.Count; i++)
        {
            answerButtons[i].gameObject.SetActive(true);
            answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = answers[i];
        }
    }

    public void TickMe()
    {
        debugText.text = Reverse(globalDialogue.Substring(globalDialogue.Length - Mathf.Clamp(currentLetter, 0, globalDialogue.Length)));
    }

    private void FixedUpdate()
    {
        if (hasToTick)
        {
            TickMe();
            currentLetter += textSpeed;
            canAcceptInput = false;
            if (currentLetter > globalDialogue.Length + (textSpeed))
            {
                hasToTick = false;
                canAcceptInput = true;
                currentLetter = 0;
            }
        }
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private void OnDestroy()
    {
        DialogueBuffer.dialogueDisplayer = null;
    }
}
