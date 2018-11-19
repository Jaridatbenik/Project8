using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dit script zit op een leeg GameObject met een collider, die een pingpongBat als child heeft.
public class GrabPongBat : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    private MeshRenderer mr_left;
    private MeshRenderer mr_right;

    GameObject explSprite;
    BallRespawn ballRespawn;

    private bool onlyOnce = true;

       
    void Start()
    {
        //Sla de MeshRenderers van de linker -en rechterhand op in variabelen, voor later gebruik.
        mr_left = leftHand.GetComponent<MeshRenderer>();
        mr_right = rightHand.GetComponent<MeshRenderer>();

        //Pak de sprite die boven de lege pingpongBat staat (deze moet later uit, vandaar).
        explSprite = GameObject.Find("ExplanationSprite");

        //Sla het script in een variabel op om die later te kunnen gebruiken.
        ballRespawn = GameObject.FindGameObjectWithTag("PingPongBall").GetComponent<BallRespawn>();
    }

    void OnTriggerStay(Collider other)
    {
        //Een TriggerStay wordt meerdere keren aangeroepen, maar de onderstaande code moet 1x ...-
        //...-aangeroepen worden.

        //Er wordt hier een boolcheck gedaan. Verder in de code gaat de bool uit.
        if(onlyOnce)
        {
            //Collision met linker-hand en hairTrigger van linker controller down...
            if (other.gameObject == leftHand && SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown())
            {
                //Doe de meshrenderer van de hand uit, zet de child daarvan (pingpongbat) aan.
                //De explanationSprite moet ook uit en de bal moet respawnen.
                mr_left.enabled = false;
                leftHand.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
                onlyOnce = false;
                explSprite.SetActive(false);
                ballRespawn.vanishing = true;
            }

            //Collision met rechter-hand en hairTrigger van rechter controller down...
            if (other.gameObject == rightHand && SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown())
            {
                //Doe de meshrenderer van de hand uit, zet de child daarvan (pingpongbat) aan.
                //De explanationSprite moet ook uit en de bal moet respawnen.  
                mr_right.enabled = false;
                rightHand.transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
                onlyOnce = false;
                explSprite.SetActive(false);
                ballRespawn.vanishing = true;
            }            
        }        
    }
}