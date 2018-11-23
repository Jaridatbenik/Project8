using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dit script zit op alle drum-hitboxes.
//(bij Hi-Hat zit ie op het child, aangezien die de hitboxx heeft).
public class Drummen : MonoBehaviour {

    //Alle mogelijke geluiden.
    public AudioClip geluidje;          //Normale hit.
    public AudioClip drumroll;          //Roffel.
    public AudioClip hihatOpen;         //Hi-Hat open
    public AudioClip hihatOpenRoll;     //Hi-Hat open-Roffel

    //BoolChecks
    public bool isHihat = false;
    public bool hihatIsOpen = false;
    private bool inroll = false;
    
    DrumKleuren drumKleuren;
    int drumIndex;

    private void OnTriggerEnter(Collider other)
    {
        //Als het object Hi-Hat is, wordt er gecheckt of de hi-hat open is of niet.
        //Daarna wordt er gecheckt of er normaal wordt geslagen of geroffelt.
        if(isHihat)
        {
            if(hihatIsOpen)
            {      
                //Als er normaal wordt geslagen (met de drumstock, natuurlijk).          
                if (other.gameObject.tag == "drumstok" && inroll == false)
                {
                    //De AudioSource clip wordt vervangen met het bijbehorende geluid, loop gaat uit.
                    GetComponent<AudioSource>().clip = hihatOpen;
                    GetComponent<AudioSource>().loop = false;
                    GetComponent<AudioSource>().Play();
                }
                //Als er wordt geroffelt en de AudioSource is not niet bezig...
                if (other.gameObject.tag == "drumstok" && inroll == true && GetComponent<AudioSource>().isPlaying == false)
                {
                    //De AudioSource clip wordt vervangen met het bijbehorende geluid, loop gaat aan.
                    GetComponent<AudioSource>().clip = hihatOpenRoll;
                    GetComponent<AudioSource>().loop = true;
                    GetComponent<AudioSource>().Play();
                }
            }
            else if (!hihatIsOpen)
            {
                if (other.gameObject.tag == "drumstok" && inroll == false)
                {
                    GetComponent<AudioSource>().clip = geluidje;
                    GetComponent<AudioSource>().loop = false;
                    GetComponent<AudioSource>().Play();
                }

                if (other.gameObject.tag == "drumstok" && inroll == true && GetComponent<AudioSource>().isPlaying == false)
                {
                    GetComponent<AudioSource>().clip = drumroll;
                    GetComponent<AudioSource>().loop = true;
                    GetComponent<AudioSource>().Play();
                }
            }        
        }
        else if(!isHihat)
        {
            if (other.gameObject.tag == "drumstok" && inroll == false)
            {
                GetComponent<AudioSource>().clip = geluidje;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
            }

            if (other.gameObject.tag == "drumstok" && inroll == true && GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().clip = drumroll;
                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().Play();
            }
        }

        //Bij trigger wordt dit aangeroepen.
        IGotHit();

    }

    private void OnTriggerStay(Collider other)
    {
        //Indien dit een Hi-Hat is, wordt er gecheckt of deze open is of niet.
        if(isHihat)
        {
            if(hihatIsOpen)
            {
                if (other.gameObject.tag == "drumstok" && inroll == true)
                {
                    GetComponent<AudioSource>().clip = hihatOpenRoll;
                    GetComponent<AudioSource>().loop = true;
                }
            }
            else if(!hihatIsOpen)
            {
                if (other.gameObject.tag == "drumstok" && inroll == true)
                {
                    GetComponent<AudioSource>().clip = drumroll;
                    GetComponent<AudioSource>().loop = true;
                }
            }
            
        }
        else if(!isHihat)
        {
            if (other.gameObject.tag == "drumstok" && inroll == true)
            {
                GetComponent<AudioSource>().clip = drumroll;
                GetComponent<AudioSource>().loop = true;
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        //Als je de stok van de trom afhaalt, moet het geluid meteen stoppen.
        GetComponent<AudioSource>().loop = false;
    }

    private void IGotHit()
    {
        //Geef de Drum-Index door aan het "DrumKleuren"-script.
        if(name == "SnareDrum")
        {
            drumIndex = 0;
        } else if(name == "Tom1")
        {
            drumIndex = 1;
        } else if(name == "Tom2")
        {
            drumIndex = 2;
        } else if(name == "Tom3")
        {
            drumIndex = 3;
        } else if(isHihat)
        {
            drumIndex = 4;
        }
        try
        {
            drumKleuren.DrumGotHit(drumIndex);
        }catch { }
    }

    private void Update()
    {
        //Als je de rechter trigger inhoudt, gaat er een bool aan. Hiermee wordt de roffel gecheckt.
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTrigger())
        {
            inroll = true;
        }
        //Die bool gaat uit als je de knop loslaat.
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp())
        {
            inroll = false;
        }

        //Hetzelfde werkt voor de linker trigger, maar dan voor het opendoen van de Hi-Hat.
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTrigger())
        {
            hihatIsOpen = true;
        }
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
        {
            hihatIsOpen = false;
        }        
    }

    void Start()
    {
        //Hi-Hat bool gaat aan, voor als het in de Inspector vergeten wordt.
        if (gameObject.name == "Hi-Hat")
            isHihat = true;     
    }
}