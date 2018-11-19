using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumKleuren : MonoBehaviour
{    
    GameObject drumSet;         //Pak de DrumSet GameObject zodat de childs in een list kunnen.
    int currentColored;         //currentColored zal de huidige gekleurde trom index aangeven.

    private Color standardColor = new Color(1, 1, 1, 1);    //1,1,1,1 is wit. Dit slaan we op om de code te verkorten.

    //Snare = 0, tom 1, 2 en 3 zijn hetzelfde. Hi-Hat = 4.
    public List<GameObject> drums = new List<GameObject>();     //Alle drums worden in de Inspector assignt.
    public List<Color> colors = new List<Color>();              //De startkleuren van de trommen (en cymbal).
    public List<int> drumOrder = new List<int>();               //De volgorde waarin de trommen van kleuren moeten wisselen.

    
    void Start()
    {
        //Pad de DrumSet door het te zoeken op naam.
        drumSet = GameObject.Find("DrumSet");

        //Pak de kleuren van de trommels en stop ze in de list zodat ze later weer gebruikt kunnen worden.
        for(int i = 0; i < drums.Count; i++)
        {
            colors.Add(drumSet.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.color);
        }
        
        //In de start wordt de eerste doorgegeven. dit is de Hi-Hat (4)
        GiveColor(4);
    }

    void GiveColor(int thisOne)
    {
        //Verander alle drums in de standardColor (wit).
        for(int i = 0; i < drums.Count; i++)
        {
            drums[i].GetComponent<MeshRenderer>().material.color = standardColor;
        }

        //Kleur de juiste drum.
        drums[thisOne].GetComponent<MeshRenderer>().material.color = colors[thisOne];
        currentColored = thisOne;
    }

    //drumHit is de trom die geslagen wordt. "Drummen"-script geeft dit.
    public void DrumGotHit(int drumHit)
    {
        //Als de juiste trom geslagen is...-
        if(drumHit == currentColored)
        {
            //Wis de eerste in de lijst.            
            drumOrder.RemoveAt(0);

            //Als de lijst NIET leeg is, roep GiveColor() aan en kleur de volgende.
            if(drumOrder.Count != 0)
            {
                Debug.Log(drumOrder[0]);
                GiveColor(drumOrder[0]);
            }

            //Als de lijst leeg is, is de game 'voorbij'.
            else if(drumOrder.Count == 0)
            {
                DrumGameDone();
            }            
        }
    }

    public void DrumGameDone()
    {
        //Als de drumGame klaar is, krijgen alle drums hun kleur.
        for (int i = 0; i < drums.Count; i++)
        {
            drums[i].GetComponent<MeshRenderer>().material.color = colors[i];
        }
    }


    //Update wordt alleen voor Debugging gebruikt.
    void Update()
    {
        Debugging();   
    }
    //Debugging Method.
    private void Debugging()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DrumGotHit(currentColored);
        }
    }
}