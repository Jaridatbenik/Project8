using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftVRControllerKnop : MonoBehaviour {

    public GameObject hand;
    Handler handler;

    public bool isInPickUp = false;

    private void Update()
    {
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
        {

            Debug.Log("druk links");
            isInPickUp = false;

            try
            {

                hand.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
                hand.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).velocity * handler.throwMultiplier;
                hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().angularVelocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).angularVelocity;

                //hand.transform.GetComponent<Rigidbody>().velocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).velocity;
                // hand.transform.GetComponent<Rigidbody>().angularVelocity = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).angularVelocity;
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
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown() && isInPickUp == false && other.gameObject.tag != "Picked" && other.gameObject.tag != "Hands")
        {
            isInPickUp = true;
            try
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.gameObject.tag = "Picked";
                other.transform.SetParent(hand.transform);
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
