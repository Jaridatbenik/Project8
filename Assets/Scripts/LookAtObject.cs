using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class LookAtObject : MonoBehaviour
{

    SteamVR_LaserPointer pointer;

    void Start()
    {
        pointer = GetComponent<SteamVR_LaserPointer>();
    }

    void Update()
    {
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown())
        {
            
        }
    }

    static bool WantsToQuit()
    {
        Debug.Log("Player prevented from quitting.");
        return false;
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.wantsToQuit += WantsToQuit;
    }
}
