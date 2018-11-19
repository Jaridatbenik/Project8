using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PongScore : MonoBehaviour
{
    public int playerScore;
    public int enemyScore;

    public void UpdateScore(int p, int e)
    {
        //Update de score
        playerScore += p;
        enemyScore += e;
        GameObject.Find("ScoreText").GetComponent<Text>().text = playerScore + " - " + enemyScore;
    }
}