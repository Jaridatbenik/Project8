using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTeleportPoint : MonoBehaviour {

    SteamVR_LaserPointer point;
    public GameObject obj;

	// Use this for initialization
	void Start () {
        point = GetComponent<SteamVR_LaserPointer>();
        
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, obj.transform.position, Color.green, 5f);
        if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp())
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, obj.transform.position, out hit, 100000f))
            {
                FindObjectOfType<TeleportHandler>().TeleportToLocation(hit.point);
                Debug.Log(hit.point);
            }

        }
        }
}
