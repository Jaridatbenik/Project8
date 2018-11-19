using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBallBlock : MonoBehaviour
{
    public int score = 0;

    GameObject scoreCol;
    Collider blockCol;
    GameObject explSprite;

    public List<GameObject> banned = new List<GameObject>();

    void Start()
    {
        //Pak de nodige GameObjects en component.
        scoreCol = GameObject.Find("ScoreCollider");
        explSprite = GameObject.Find("ExplanationSprite");
        blockCol = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Als de bal de collider raakt met positieve velocity...-
        if (other.gameObject.tag == "Unpicke" && other.gameObject.GetComponent<Rigidbody>().velocity.y > 0)
        {
            //Voeg de bal toe aan de banlist.        
            banned.Add(other.gameObject);
        }
        //Als de niet-gebande bal door de basket gaat met negatieve velocity
        if (other.gameObject.tag == "Unpicke" && other.gameObject.GetComponent<Rigidbody>().velocity.y < 0 && !banned.Contains(other.gameObject))
        {
            //Score gaat omhoog en de bal gaat meteen de banlist in.
            score++;
            banned.Add(other.gameObject);

            //ScoreText moet worden ge-update.
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Score:\n" + score;

            //De explanationSprite gaat weg, de speler weet inmiddels hoe het spel werkt.
            try
            {
                if (explSprite.activeInHierarchy)
                {
                    explSprite.SetActive(false);
                }
            }catch { }
        }
    }

    void Update()
    {
        //Als de bal in de hand wordt gehouden, gaat deze uit de banlist.
        try
        {
            for (int i = 0; i < banned.Count; i++)
            {
                if (banned[i].tag == "Picked")
                {
                    banned.RemoveAt(i);
                }
            }
        }
        catch
        {

        }
    }
}