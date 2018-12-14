using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public List<object> dialogue = new List<object>();
    public string startID;
}

public class Sentence
{
    public string id;
    public string scentence;
    public string nextNode;

    public Sentence(string scentence, string nextNode, string id)
    {
        this.scentence = scentence;
        this.nextNode = nextNode;
        this.id = id;
    }
}

public class Question
{
    public string id;
    public string question;
    public List<string> answers = new List<string>();
    public List<string> nodes = new List<string>();
    
    public Question(string question, List<string> answers, List<string> nodes, string id)
    {
        this.question = question;
        this.answers = answers;
        this.nodes = nodes;
        this.id = id;
    }
}

public static class DialogueBuffer
{
    public static List<Dialogue> dialogueBuffer = new List<Dialogue>();
    public static DialogueDisplayer dialogueDisplayer;

    public static void Load(Dialogue log)
    {
        dialogueBuffer.Add(log);
    }

    public static void DiscardFirst()
    {
        dialogueBuffer.RemoveAt(0);
    }

    public static void DiscardAll()
    {
        dialogueBuffer.Clear();
    }

    public static void PushFirst()
    {
        dialogueDisplayer.DisplayDialogue(dialogueBuffer[0]);
        dialogueBuffer.RemoveAt(0);
    }
}

