using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drummen_HiHat : MonoBehaviour
{
    public AudioClip hhClosedHit;
    public AudioClip hhClosedRoll;

    public AudioClip hhOpenHit;
    public AudioClip hhOpenRoll;

    private bool inroll = false;
    private bool hhClosed = true;

    private Color closedColor;
    public Color openColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "drumstok" && inroll == false)
        {  
            if(hhClosed)
            {
                GetComponent<AudioSource>().clip = hhClosedHit;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
            }
            else if(!hhClosedRoll)
            {
                GetComponent<AudioSource>().clip = hhOpenHit;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
            }            
        }

        if (other.gameObject.tag == "drumstok" && inroll == true && GetComponent<AudioSource>().isPlaying == false)
        {
            if(hhClosed)
            {
                GetComponent<AudioSource>().clip = hhClosedRoll;
                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().Play();
            }
            else if(!hhClosed)
            {
                GetComponent<AudioSource>().clip = hhOpenRoll;
                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().Play();
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "drumstok" && inroll == true)
        {
            if(hhClosed)
            {
                GetComponent<AudioSource>().clip = hhClosedRoll;
                GetComponent<AudioSource>().loop = true;
            }
            else if(!hhClosed)
            {
                GetComponent<AudioSource>().clip = hhOpenRoll;
                GetComponent<AudioSource>().loop = true;
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<AudioSource>().loop = false;
    }


    private void Update()
    {
        ChangeColor();
        playerInput();

        Debug.Log(hhClosed);  
    }

    void Start()
    {
        closedColor = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    void ChangeColor()
    {
        if(hhClosed)
        {
            gameObject.transform.parent.GetComponent<MeshRenderer>().material.color = closedColor;
        }
        if(!hhClosed)
        {
            gameObject.transform.parent.GetComponent<MeshRenderer>().material.color = openColor;
        }
    }

    void playerInput()
    {
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTrigger())
        {
            inroll = true;
        }
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp())
        {
            inroll = false;
        }


        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTrigger())
        {
            hhClosed = false;
        }
        else if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
        {
            hhClosed = true;
        }
    }
}