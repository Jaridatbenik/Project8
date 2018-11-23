using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class ObjectPicker : MonoBehaviour
{
    SteamVR_LaserPointer pointer;

    public GameObject currentSelected;
    public Targetable currentTarget;

    public bool isOnObject = false;

    void Start()
    {
        pointer = GetComponent<SteamVR_LaserPointer>();
        pointer.PointerIn -= HandlePointerIn;
        pointer.PointerIn += HandlePointerIn;
        pointer.PointerOut -= HandlePointerOut;
        pointer.PointerOut += HandlePointerOut;
    }

    void Update()
    {
        if(currentSelected != null)
        {
            if (isOnObject)
            {
                currentSelected.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }else
            {
                if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown())
                {
                    return;
                }
            }


            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown())
            {
                currentSelected.SetActive(false);
                currentTarget.referenceObject.SetActive(true);
                currentTarget.referenceObject.transform.SetParent(transform);
                currentTarget.referenceObject.transform.localPosition = new Vector3(0, 0, 0.2f);
                pointer.enabled = false;
            }
            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp())
            {
                currentSelected.SetActive(true);
                currentTarget.referenceObject.SetActive(false);
                currentTarget.referenceObject.transform.SetParent(null);
                currentTarget.referenceObject.transform.position = new Vector3(0, 0, 0);
                pointer.enabled = true;
            }
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.GetComponent<Targetable>() != null)
        {
            currentSelected = e.target.gameObject;
            currentTarget = e.target.gameObject.GetComponent<Targetable>();
            isOnObject = true;
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        if (currentSelected != null)
        {
            currentSelected.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
            isOnObject = false;
        
    }
}
