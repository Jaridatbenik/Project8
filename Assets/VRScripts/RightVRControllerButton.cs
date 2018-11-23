using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightVRControllerButton : MonoBehaviour {

    public GameObject hand;
    public bool isInPickUp = false;
    Handler handler;


    //het script om handen te laten werken
    private void Update()
    {
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp())
        {
            Debug.Log("druk");
            //nou als je de trigger loslaat mag je weer dingen oppakken
            isInPickUp = false;

            try
            {
                //dan zet hij de childobject van het ding dat hij vast heeft los en zet hij de gravity weer aan en dan doet hij rigidbody weer aan enzo.
                hand.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
                hand.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //hier zet ik de velocity van het object gelijk aan de velocity enzo van de controller
                hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).velocity * handler.throwMultiplier;
                hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().angularVelocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).angularVelocity;

                hand.transform.GetChild(0).tag = "Unpicke";
                hand.transform.DetachChildren();
            }
            catch
            {

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //als je met je hand in de collider van de bal ofzo zit en je drukt de trigger in
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() && isInPickUp == false && other.gameObject.tag != "Picked" && other.gameObject.tag != "Hands")
        {
            Debug.Log("druk");
            //dan kan je oppaken
            isInPickUp = true;
            try
            {
                //nou van dat object zet je de zwaartekracht it en mag het object niet bewegen
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //other.gameObject.GetComponent<Rigidbody>().velocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).velocity;
                //other.gameObject.GetComponent<Rigidbody>().angularVelocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).angularVelocity;
                other.transform.SetParent(hand.transform);
                other.gameObject.tag = "Picked";
            }catch
            {

            }
        }
    }

    void Start()
    {
        handler = GameObject.Find("MainGameLogic").GetComponent<Handler>();
    }
}
