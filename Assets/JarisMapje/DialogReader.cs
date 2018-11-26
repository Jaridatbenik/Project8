using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DialogReader
{

    static Dialogue dialog;
    static string pathSceneFile;
    static string pathDiaFile;
    static string[] lines;
    static string[] diagLines;

    static int start, end;

    public static void InitScenario(string scenario)
    {
        CleanUp();

        pathSceneFile = Application.dataPath + "/StreamingAssets/Dialogue/Scenario/" + scenario + ".sce";
        lines = File.ReadAllLines(pathSceneFile);
        SetStartAndEndPoint("Init");
        for (int i = start; i <= end; i++)
        {
            if (lines[i].Contains("<DialogFile>"))
            {
                pathDiaFile = (Application.dataPath + "/StreamingAssets/Dialogue/Script/" + lines[i].Split(':')[1] + ".kdialog");
                break;
            }
        }
        PopulateDictionary();
    }

    public static void PopulateDictionary()
    {
        dialog = new Dialogue();
        diagLines = File.ReadAllLines(pathDiaFile);

        SetStartAndEndPoint("Script");

        for (int i = start; i <= end; i++)
        {
            if (lines[i].StartsWith("<"))
            {
                //GetText() gets the lines from the .kdialog file using the pointer from the .sce file. 
                //lines[i].Split(':')[0] gets the pointer from the .sce file
                //lines[i].Split(':')[1].Trim() gets the next pointer for the next line of dialogue
                //lines[i].Split(':')[0].Trim() at the end gets its current pointer and it sets the scentence id
                dialog.dialogue.Add(new Sentence(GetText(lines[i].Split(':')[0]).Trim(), lines[i].Split(':')[1].Trim(), lines[i].Split(':')[0].Trim()));
            }
            if (lines[i].StartsWith("("))
            {
                dialog.dialogue.Add(new Question(GetText(lines[i].Split(':')[0]).Split('@')[0].Trim(), GetAnswers(GetText(lines[i].Split(':')[0]).Trim()), GetNodes(lines[i].Split(':')[1].Trim()), lines[i].Split(':')[0].Trim()));
            }
            if (lines[i].Contains("["))
            {
                if (lines[i].StartsWith("[START]"))
                {
                    dialog.startID = lines[i].Split(':')[1].Trim();
                }
            }
        }
        DialogueBuffer.Load(dialog);
    }

    static void SetStartAndEndPoint(string type)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith(type))
            {
                for (int f = i; f < lines.Length; f++)
                {
                    if (lines[f].Contains("{"))
                    {
                        start = f;
                    }
                    if (lines[f].Contains("}"))
                    {
                        end = f;
                        break;
                    }
                }
                break;
            }
        }
    }

    static string GetText(string comparer)
    {
        string temp = "";
        for (int i = 0; i < diagLines.Length; i++)
        {
            if (diagLines[i].Contains(comparer))
            {
                temp = diagLines[i].Split(':')[1];
                break;
            }
        }
        return temp;
    }

    static List<string> GetAnswers(string str)
    {
        List<string> answers = new List<string>();

        for (int i = 1; i < str.Split('@').Length; i++)
        {
            answers.Add(str.Split('@')[i].Trim());
        }

        return answers;
    }

    static List<string> GetNodes(string str)
    {
        List<string> nodes = new List<string>();

        try
        {
            for (int i = 0; i < str.Split(',').Length; i++)
            {
                nodes.Add(str.Split(',')[i].Trim());
            }
        }
        catch { }

        return nodes;
    }

    static void CleanUp()
    {
        pathSceneFile = "";
        pathDiaFile = "";
        lines = new string[0];
        diagLines = new string[0];
    }

}
